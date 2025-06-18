namespace MS_Authentication.Infrastructure.Scripts;

public class UserScript
{
    internal static readonly string UserExist =
        @"
            SELECT 
                COUNT(*)
            FROM
                users
            WHERE 
                Id = @Id
                AND DeletedOn IS NULL
        ";
}
