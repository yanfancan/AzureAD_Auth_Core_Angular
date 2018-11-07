using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSC.RTD.AVLAggregatorCore.Data
{
    public partial class sql_AvlRepository
    {
        private static string sql_selectSession = "select top 1 s.*,sa.Roles, sa.TimeZone from Sessions s inner join ServiceAccounts sa (nolock) on s.ServiceAccountId = sa.Id";

        public static string sql_NewSession = $@"update Sessions set ModifiedBy = @AddedBy, ModifiedDateTime = getutcdate(), LastRequestDateTime = getutcdate() where ServiceAccountId = @ServiceAccountId and Status = 1;
                                                    IF @@ROWCOUNT=0 insert into Sessions (Status, ServiceAccountId, ClientIp, AddedBy) values(1, @ServiceAccountId, @ClientIp, @AddedBy);
                                                    {sql_selectSession} where s.ServiceAccountId = @ServiceAccountId and s.Status = 1";

        public static string sql_UpdateSession = $@"update Sessions set ModifiedBy = @ModifiedBy, ModifiedDateTime = getutcdate(), LastRequestDateTime = getutcdate(), Status = @Status, 
                                                        EndDateTime = case when @Status = 2 then getutcdate() else null end  where Id = @SessionId;
                                                    {sql_selectSession} where s.Id = @SessionId";

        public static string sql_GetSessionsbyClientIpName = $@"{sql_selectSession} where s.ClientIp = @ClientIp and sa.LoginName = @LoginName";

        //this query will fire 'INSTEADOF_TR_Positions_Insert' trigger
        public static string sql_UpdatePosition = @"INSERT INTO [Positions] ([Address],[Latitude],[Longitude],[Velocity],[Direction],[AvlDateTime],[ModifiedDateTime],[ModifiedBy])
                                                           VALUES (@Address,@Latitude,@Longitude,@Velocity,@Direction,@AvlDateTime,getutcdate(),@ModifiedBy)";

        public static string sql_GetChangedPositionsByServiceAccount = @"declare @currentDate datetime = getutcdate(); 
                                                                    declare @gz geography;
                                                                    select @gz = GeoZone from ServiceAccounts where Id = @ServiceAccountId;
                                                                    declare @result table
                                                                    (
	                                                                    Id int,
	                                                                    Address varchar(150),
	                                                                    Latitude decimal(18, 6),
	                                                                    Longitude decimal(18, 6),
	                                                                    Velocity int,
	                                                                    Direction int,
	                                                                    Point geography,
	                                                                    AvlDateTime datetime,
	                                                                    ModifiedDateTime datetime,
	                                                                    ModifiedBy varchar(150),
																		VehicleName varchar(50)
                                                                    );

                                                                    insert into @result
                                                                    select p.*, d.VehicleName from [Positions] p (nolock)
                                                                           left join Devices d (nolock) on d.Address = p.Address
                                                                           left join ServiceAccounts_Services ss  (nolock) on ss.ServiceId = d.ServiceId
	                                                                       left join ServiceAccounts sa (nolock) on sa.Id = ss.ServiceAccountId
                                                                           where (ss.ServiceAccountId = @ServiceAccountId or (@gz is not null and @gz.STIntersects(p.Point) = 1))
	                                                                         and (@MaxFeedAgeSec = 0 or datediff(ss, p.ModifiedDateTime, @currentDate) <= @MaxFeedAgeSec);

                                                                    update ds set LastReportDateTime = @currentDate
                                                                        from  DeviceStats as ds 
                                                                        inner join @result r on r.Address = ds.Address

                                                                    select * from @result;";

    }
}