USE [SHINE]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertClient]    Script Date: 02/07/2019 12:25:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_InsertClient]
(
  @Name VARCHAR(50),
  @LastName VARCHAR(50),
  @Username VARCHAR(50),
  @Password  	VARCHAR(50),
  @Email VARCHAR(50),
  @PhoneNumber VARCHAR(50),
  @FavoriteGame VARCHAR(50),
  @BirthDate DateTime,
  @Gender VARCHAR(50), 
  @RegisteredDate DATETIME
)
AS
BEGIN

SET NOCOUNT ON;

INSERT INTO Client 
(
Name,
LastName,
Username,
Password,
Email,
PhoneNumber,
FavoriteGame,
BirthDate,
Gender,
RegisteredDate
) VALUES 
(
@Name,
@LastName,
@Username,
@Password,
@Email,
@PhoneNumber,
@FavoriteGame,
@BirthDate,
@Gender,
@RegisteredDate
);

END
