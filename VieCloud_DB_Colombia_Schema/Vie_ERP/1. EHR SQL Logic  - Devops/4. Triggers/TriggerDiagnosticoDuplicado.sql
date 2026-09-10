
USE [INDIGOxxx]
GO
/****** Object:  Trigger [dbo].[DetectarDobleDiagnosticosEnLaHistoria]    Script Date: 20/02/2017 9:30:35 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create trigger [dbo].[DetectarDobleDiagnosticosEnLaHistoria]
on [dbo].[INDIAGNOP]
for insert,update as
begin
    declare @Ingreso as varchar(10) = (select  NUMINGRES  from inserted)
	declare @NUMEFOLIO as varchar(10) = (select NUMEFOLIO   from inserted)
			
	   declare @ExisteDiagnosticoenIndiagnop int
			select @ExisteDiagnosticoenIndiagnop = count(*) from INDIAGNOP where NUMINGRES = @Ingreso AND NUMEFOLIO  = @NUMEFOLIO AND CODDIAPRI = 1
			if @ExisteDiagnosticoenIndiagnop > 1 begin
				  raiserror('Contacte administrador sistemas: no puede llegar Grabar dos diagnosticos principales!',10,1)
	              rollback transaction
			end
end

