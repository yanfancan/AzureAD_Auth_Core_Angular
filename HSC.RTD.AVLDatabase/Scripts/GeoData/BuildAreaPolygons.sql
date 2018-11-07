/*
This script extracts data from [ResponseAreaSegments] table, generates geography polygons for every AreaId and saves them into table [ResponseAreaPolygons]
Expected execution tume 5-10 minutes depending on the number of Areas;
*/

declare @AvlServiceAccountId int = 12; -- ServiceAccounts.Id for which GeoZone will be generated


--Step 1 Populate [ResponseAreaPolygons]
declare @areaid int;
declare @pol geography;

delete from ResponseAreaPolygons;

declare a_cursor cursor
	for select distinct AreaId from ResponseAreaSegments order by AreaId;
open a_cursor
fetch next from a_cursor
into @areaid
while @@FETCH_STATUS = 0
begin
	declare @polstr nvarchar(max) = null;
	select @polstr = coalesce(@polstr + ',','')+ cast(-Lon/1000000 as nvarchar(50))+ ' ' +cast(Lat/1000000 as nvarchar(50))
	from ResponseAreaSegments where AreaId = @areaid order by DrawOrder
	set @polstr = 'POLYGON((' + @polstr + '))';
	set @pol = geography::STPolyFromText(@polstr, 4955);
	insert into ResponseAreaPolygons (AreaId, Polygon) values (@areaId, case when @pol.MakeValid().EnvelopeAngle() = 180 then @pol.ReorientObject() else @pol end);
	print @pol.STAsText();		

	fetch next from a_cursor
into @areaid

end;
close a_cursor;
deallocate a_cursor;

--Step 2 Generate GeoZone from polygons in [ResponseAreaPolygons] and save it into [ServiceAccounts]
DECLARE @mPol geography;  
select @mPol = geography::UnionAggregate(Polygon.MakeValid()) from  [dbo].[ResponseAreaPolygons]
print @mPol.STAsText();

-- Save the calculated polygon
-- Calculate the GeoZone extending Polygon by 5km
update [dbo].[ServiceAccounts] set Polygon = @mPol, GeoZone = @mPol.Reduce(1000).STBuffer(5000).Reduce(100) where Id = @AvlServiceAccountId