CREATE PROCEDURE [dbo].[EnablePerson]
@IdPerson uniqueidentifier
AS
BEGIN
	UPDATE Person SET IsActive = 1
	WHERE IdPerson = @IdPerson
END

GO