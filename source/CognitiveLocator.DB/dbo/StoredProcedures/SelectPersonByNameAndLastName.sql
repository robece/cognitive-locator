CREATE PROCEDURE [dbo].[SelectPersonByNameAndLastName]
@Name nvarchar(500),
@LastName nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT IdPerson, IsFound, Name, LastName, Alias, Age, BirthDate,ReportedBy,Picture, Location, Notes, IsActive, FaceId FROM Person WHERE UPPER(Name) LIKE UPPER('%'+@Name+'%') AND UPPER(LastName) LIKE UPPER('%'+@LastName+'%')
END
GO