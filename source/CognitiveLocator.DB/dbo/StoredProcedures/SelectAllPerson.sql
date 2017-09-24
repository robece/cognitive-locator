CREATE PROCEDURE [dbo].[SelectAllPerson]

AS
BEGIN
	SET NOCOUNT ON;
	SELECT Name,LastName,Alias,BirthDate,ReportedBy,Picture,Location,Notes,FaceId FROM Person WHERE IsActive = 1
END
GO