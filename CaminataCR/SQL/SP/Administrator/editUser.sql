CREATE PROCEDURE [dbo].[edituser]
	@oldUserName varchar(20),
	@newUserName varchar(20)
AS
	DECLARE @alreadyExists int
	SET @alreadyExists = (SELECT COUNT(*) FROM Usuario  WHERE @newUserName = cuenta)
	IF @alreadyExists = 0
    BEGIN        
		UPDATE Usuario SET cuenta = @newUserName  WHERE cuenta = @oldUserName
		RETURN(0)
    END
    ELSE
       RETURN(1)