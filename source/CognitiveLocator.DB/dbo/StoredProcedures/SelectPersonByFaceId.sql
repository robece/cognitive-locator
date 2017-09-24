CREATE PROCEDURE [dbo].[SelectPersonByFaceId]
@FaceId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	SELECT IdPerson, IsFound, Name, LastName, Alias, Age,BirthDate,ReportedBy, Picture, Location, Notes, IsActive, FaceId FROM Person WHERE FaceId = @FaceId
END

GO