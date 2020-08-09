
CREATE DATABASE TheTaskDb2;

USE TheTaskDb2;

CREATE TABLE DEPT
       (DEPTNO DECIMAL(30) CONSTRAINT PK_DEPT PRIMARY KEY,
	DNAME VARCHAR(14) ,
	LOC VARCHAR(13) ) ;

CREATE TABLE EMP
       (EMPNO DECIMAL(30) CONSTRAINT PK_EMP PRIMARY KEY,
	ENAME VARCHAR(10),
	JOB VARCHAR(9),
	MGR DECIMAL(4),
	HIREDATE DATE,
	SAL DECIMAL(7,2),
	COMM DECIMAL(7,2),
	DEPTNO DECIMAL(30) CONSTRAINT FK_DEPTNO REFERENCES DEPT);

INSERT INTO DEPT VALUES
	(10,'ACCOUNTING','NEW YORK');
INSERT INTO DEPT VALUES (20,'RESEARCH','DALLAS');
INSERT INTO DEPT VALUES
	(30,'SALES','CHICAGO');
INSERT INTO DEPT VALUES
	(40,'OPERATIONS','BOSTON');
INSERT INTO EMP VALUES
(7369,'SMITH','CLERK',7902,Datefromparts(1980, 01, 11),800,NULL,20);
INSERT INTO EMP VALUES
(7499,'ALLEN','SALESMAN',7698,Datefromparts(1970, 11, 21),1600,300,30);
INSERT INTO EMP VALUES
(7521,'WARD','SALESMAN',7698,Datefromparts(1981, 01, 11),1250,500,30);
INSERT INTO EMP VALUES
(7566,'JONES','MANAGER',7839,Datefromparts(1981, 04, 05),2975,NULL,20);
INSERT INTO EMP VALUES
(7654,'MARTIN','SALESMAN',7698,Datefromparts(1980, 02, 04),1250,1400,30);
INSERT INTO EMP VALUES
(7698,'BLAKE','MANAGER',7839,Datefromparts(1981, 03, 08),2850,NULL,30);
INSERT INTO EMP VALUES
(7782,'CLARK','MANAGER',7839,Datefromparts(1987, 06, 13),2450,NULL,10);
INSERT INTO EMP VALUES
(7788,'SCOTT','ANALYST',7566,Datefromparts(1983, 07, 05),3000,NULL,20);
INSERT INTO EMP VALUES
(7839,'KING','PRESIDENT',NULL,Datefromparts(1990, 01, 11),5000,NULL,10);
INSERT INTO EMP VALUES
(7844,'TURNER','SALESMAN',7698,Datefromparts(1985, 10, 08),1500,0,30);
INSERT INTO EMP VALUES
(7876,'ADAMS','CLERK',7788,Datefromparts(1980, 11, 17),1100,NULL,20);
INSERT INTO EMP VALUES
(7900,'JAMES','CLERK',7698,Datefromparts(1988, 01, 11),950,NULL,30);
INSERT INTO EMP VALUES
(7902,'FORD','ANALYST',7566,Datefromparts(1980, 05, 01),3000,NULL,20);
INSERT INTO EMP VALUES
(7934,'MILLER','CLERK',7782,Datefromparts(1982, 02, 02),1300,NULL,10);

CREATE TABLE SALGRADE
      ( GRADE int,
	LOSAL int,
	HISAL int );

INSERT INTO SALGRADE VALUES (1,700,1200);
INSERT INTO SALGRADE VALUES (2,1201,1400);
INSERT INTO SALGRADE VALUES (3,1401,2000);
INSERT INTO SALGRADE VALUES (4,2001,3000);
INSERT INTO SALGRADE VALUES (5,3001,9999);

CREATE PROCEDURE sp_AddEmp        
(                   
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
    VALUES ((SELECT MAX(EMPNO)+1 FROM EMP),@ENAME,@JOB, @MGR,@HIREDATE, @SAL, @COMM, @DEPTNO)
END  

CREATE PROCEDURE sp_DeleteEmp           
(            
   @empNo decimal            
)            
AS             
BEGIN            
   DELETE FROM EMP WHERE EMPNO= @empNo            
END  

CREATE PROCEDURE [dbo].[sp_GetEmps]
AS
    SELECT EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO FROM EMP 
GO

CREATE PROCEDURE [dbo].[sp_GetEmpsByDept] @deptNo decimal
AS
      WITH cteReports (EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO ) AS
(
    SELECT EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO
    FROM EMP
    WHERE MGR IS NULL

    UNION ALL

    SELECT e.EMPNO, e.ENAME, e.JOB, e.MGR, e.HIREDATE, e.SAL, e.COMM, e.DEPTNO
    FROM EMP        AS e
    INNER JOIN cteReports AS r ON e.MGR = r.EMPNO
)
SELECT
    EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO
FROM cteReports
	WHERE DEPTNO = @deptNo
GO

CREATE PROCEDURE [dbo].[sp_GetEmpById] @empNo decimal           
AS             
    SELECT EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO FROM EMP
	WHERE EMPNO = @empNo
GO

CREATE PROCEDURE [dbo].[sp_Subordinates] @MgrNo decimal null
AS
   SELECT EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO, SALGRADE.GRADE FROM EMP
    LEFT JOIN SALGRADE
ON SAL >= SALGRADE.LOSAL and SAL <= SALGRADE.HISAL
   WHERE MGR = @MgrNo
GO

CREATE PROCEDURE [dbo].[sp_President]
AS
   SELECT EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO, SALGRADE.GRADE FROM EMP
   LEFT JOIN SALGRADE
ON SAL >= SALGRADE.LOSAL and SAL <= SALGRADE.HISAL
   WHERE MGR IS NULL
GO

CREATE PROCEDURE [dbo].[sp_Hierarchy]
AS
  WITH cteReports (EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO ) AS
(
    SELECT EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO
    FROM EMP
    WHERE MGR IS NULL

    UNION ALL

    SELECT e.EMPNO, e.ENAME, e.JOB, e.MGR, e.HIREDATE, e.SAL, e.COMM, e.DEPTNO
    FROM EMP        AS e
    INNER JOIN cteReports AS r ON e.MGR = r.EMPNO
)
SELECT
    EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO
FROM cteReports
GO

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

CREATE PROCEDURE [dbo].[sp_GetDepts]           
AS             
    SELECT DEPTNO, DNAME, LOC FROM DEPT
GO

CREATE PROCEDURE [dbo].[sp_GetDeptById] @deptNo decimal           
AS             
    SELECT DEPTNO, DNAME, LOC FROM DEPT
	WHERE DEPTNO = @deptNo
GO


CREATE PROCEDURE sp_AddDept        
(                     
    @DNAME VARCHAR(14),          
    @LOC VARCHAR(13)                    
)          
AS           
BEGIN
    INSERT INTO DEPT (DEPTNO, DNAME, LOC)           
    VALUES ((SELECT MAX(DEPTNO)+1 FROM DEPT),@DNAME,@LOC)
END 

CREATE PROCEDURE sp_DeleteDept           
(            
   @deptNo decimal           
)            
AS             
BEGIN            
   DELETE FROM DEPT WHERE DEPTNO= @deptNo         
END 

CREATE PROCEDURE sp_UpdateDept            
(                         
    @DEPTNO DECIMAL(30),
	@DNAME VARCHAR(14),
	@LOC VARCHAR(13)
)            
AS           
BEGIN            
   UPDATE DEPT             
   SET 
   DEPTNO=@DEPTNO,            
   DNAME=@DNAME,            
   LOC=@LOC                   
   WHERE DEPTNO=@DEPTNO            
END
