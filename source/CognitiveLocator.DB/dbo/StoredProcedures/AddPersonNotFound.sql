CREATE PROCEDURE  [dbo].[AddPersonNotFound]
	@IdPerson uniqueidentifier,
	@IsFound bit = 0,
	@Name nvarchar(50),
	@LastName nvarchar(500),
	@Alias nvarchar(50) = null,
	@Age int = null,
	@BirthDate DateTime = null,
	@ReportedBy nvarchar(100),
	@Picture nvarchar(1000),
	@Location nvarchar(500) = null,
	@Notes nvarchar(1000) = null,
	@IsActive bit,
	@FaceId uniqueidentifier = null
AS
BEGIN
	INSERT INTO dbo.Person
	VALUES (@IdPerson,@IsFound,@Name,@LastName,@Alias,@Age,@BirthDate,@ReportedBy,@Picture,@Location,@Notes,GETDATE(),GETDATE(),@IsActive,@FaceId)
END
GO