CREATE PROCEDURE [dbo].[SelectAllPerson]

AS
BEGIN
	SET NOCOUNT ON;
	SELECT Name,LastName,Alias,Age,Picture,Location,Notes FROM Person WHERE IsActive = 1
END

GO