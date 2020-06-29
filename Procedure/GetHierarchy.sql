USE TheTaskDb2;
CREATE PROCEDURE [dbo].[sp_Subordinates] @MgrNo decimal null
AS
   SELECT * FROM EMP
   WHERE MGR = @MgrNo
GO
