CREATE PROCEDURE [dbo].[UpdateFoundPerson]
@IdPerson uniqueidentifier,
@IsFound Bit = 1,
@Location nvarchar(500) = null

AS
BEGIN
	UPDATE Person SET IsFound = @IsFound, Location = @Location WHERE IdPerson = @IdPerson 
END

GO
