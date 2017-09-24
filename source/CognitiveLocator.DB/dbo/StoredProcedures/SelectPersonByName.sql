CREATE PROCEDURE [dbo].[SelectPersonByName]
@Name nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT IdPerson, IsFound, Name, LastName, Alias, Age, Picture, Location, Notes, IsActive, FaceId FROM Person WHERE UPPER(Name) LIKE UPPER('%'+@Name+'%')
END

GO