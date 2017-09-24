CREATE PROCEDURE [dbo].[SelectPersonByLastName]
@LastName nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT IdPerson, IsFound, Name, LastName, Alias, BirthDate, ReportedBy,Picture, Location, Notes, IsActive, FaceId FROM Person WHERE UPPER(LastName) LIKE UPPER('%'+@LastName+'%')
END

GO