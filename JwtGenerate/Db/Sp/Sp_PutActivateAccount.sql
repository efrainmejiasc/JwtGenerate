USE [SHINE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_PutActivateAccount]
(
  @Password VARCHAR(50),
  @Email 	VARCHAR(50),
  @Code VARCHAR(10),
  @VerificationDate DATETIME,
  @Status BIT
)
AS
BEGIN

SET NOCOUNT ON;

DECLARE @Id INT;
DECLARE @Exit BIT;

          SET @EXIT = 0
          SET @Id = (SELECT MAX (ID) FROM CodeToVerification WHERE Password = @Password AND Email = @Email AND Code = @Code AND Status = 0)
		  IF (@Id > 0)
		      BEGIN
			  UPDATE Client SET  RegisteredStatus = @Status WHERE Password = @Password AND Email = @Email AND RegisteredStatus = 0
			  UPDATE CodeToVerification SET VerificationDate = @VerificationDate, Status = @Status WHERE Password = @Password AND Email = @Email AND Status = 0
			  SET @Exit = 1 
			  END
  SELECT @Exit

END;
