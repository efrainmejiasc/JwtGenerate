USE [SHINE]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetClient]    Script Date: 02/07/2019 11:39:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_GetClient]
(
  @Password VARCHAR(50),
  @Email 	VARCHAR(50)
)
AS
BEGIN

SET NOCOUNT ON;

SELECT * FROM Client WHERE Password = @Password AND Email = @Email;

END
