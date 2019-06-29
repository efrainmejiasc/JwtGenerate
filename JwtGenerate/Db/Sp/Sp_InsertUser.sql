USE [SHINE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_InsertUser]
(
  @Username VARCHAR(50),
  @Password  	VARCHAR(50),
  @Email VARCHAR(50),
  @FechaRegistro DATETIME
)
AS
BEGIN

SET NOCOUNT ON;

INSERT INTO Client (Username,Password,Email,FechaRegistro) VALUES (@Username,@Password,@Email,@FechaRegistro);

END
