USE TheTaskDb2

CREATE PROCEDURE sp_AddEmp        
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
    INSERT INTO EMP (EMPNO,ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO)           
    VALUES (@EMPNO,@ENAME,@JOB, @MGR,@HIREDATE, @SAL, @COMM, @DEPTNO)
END  
