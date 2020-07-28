USE TheTaskDb2;

CREATE PROCEDURE sp_UpdateEmp            
(            
    @EMPNO DECIMAL(30),           
    @ENAME VARCHAR(10),          
    @JOB VARCHAR(9),          
    @MGR DECIMAL(4),  
    @HIREDATE DATE,          
    @SAL DECIMAL(7,2),          
    @COMM DECIMAL(7,2),          
    @DEPTNO DECIMAL(30)             
)            
AS           
BEGIN            
   UPDATE EMP             
   SET EMPNO=@EMPNO,            
   ENAME=@ENAME,            
   JOB=@JOB,          
   MGR=@MGR,   
   HIREDATE=@HIREDATE,            
   SAL=@SAL,            
   COMM=@COMM,            
   DEPTNO=@DEPTNO            
   WHERE EMPNO=@EMPNO            
END 
