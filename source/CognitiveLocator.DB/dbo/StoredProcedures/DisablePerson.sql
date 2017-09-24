CREATE PROCEDURE [dbo].[DisablePerson]
@IdPerson uniqueidentifier
AS
BEGIN
	UPDATE Person SET IsActive = 0
	WHERE IdPerson = @IdPerson
END

GO