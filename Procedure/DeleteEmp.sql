CREATE PROCEDURE sp_DeleteEmp           
(            
   @empNo decimal            
)            
AS             
BEGIN            
   DELETE FROM EMP WHERE EMPNO= @empNo            
END  
