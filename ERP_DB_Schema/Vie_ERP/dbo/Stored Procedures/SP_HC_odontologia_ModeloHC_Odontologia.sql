
-- =============================================
-- Author:		Rafael Eduardo Patiño Cabrera
-- Create date: 09/05/2019
-- Description:	crea tabla historico para el informe de - odontologia - por x periodo de tiempo
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_odontologia_ModeloHC_Odontologia]
 @FechaInicial as date,
 @FechaFinal as date,
 @IDMODELOHC as int
AS

BEGIN

declare @table as table(ID int IDENTITY(1,1), IDHiSPACA int)

if @IDMODELOHC = 33 or @IDMODELOHC = 28 or @IDMODELOHC = 42 begin
		insert  into @table 
		select a.ID as IdHispaca
		From dbo.HCHISPACA  A
		LEFT  JOIN dbo.ODONTOCONTROL B WITH (NOLOCK) ON A.NUMEFOLIO = B.NUMEFOLIO AND A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES 
		INNER JOIN dbo.INPACIENT C WITH (NOLOCK) ON A.IPCODPACI = C.IPCODPACI 
		INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES = D.NUMINGRES 
		INNER JOIN Contract.CareGroup E WITH (NOLOCK) ON E.Id = D.GENCAREGROUP 
		INNER JOIN dbo.INENTIDAD F WITH (NOLOCK) ON F.CODENTIDA = D.CODENTIDA 
		INNER JOIN dbo.INPROFSAL G WITH (NOLOCK) ON G.CODPROSAL = A.CODPROSAL 
		LEFT JOIN dbo.ODONTOCONTROLVALO H WITH (NOLOCK) ON H.IDODONTOCONTROL = A.ID 
		INNER JOIN dbo.INDIAGNOS  I WITH (NOLOCK) ON I.CODDIAGNO  = A.CODDIAGNO
		INNER  JOIN  dbo.HCRIESGOSP J  WITH (NOLOCK) ON J.NUMINGRCES = A.NUMINGRES 
		Where A.FECHISPAC  BETWEEN @FechaInicial and @FechaFinal and a.IDMODELOHC = @IDMODELOHC
end else begin
	
		insert  into @table 
		select HC.ID as IdHispaca 
		FROM         dbo.ADINGRESO AS ING INNER JOIN
		dbo.HCHISPACA AS HC WITH (NOLOCK) ON ING.NUMINGRES =HC.NUMINGRES AND ING.IPCODPACI = HC.IPCODPACI  INNER JOIN
		dbo.HCURGING1 AS URG ON HC.NUMINGRES =URG.NUMINGRES AND HC.IPCODPACI =URG.IPCODPACI AND HC.NUMEFOLIO =URG.NUMEFOLIO  INNER JOIN
		dbo.INDIAGNOS AS DIAG WITH (NOLOCK) ON ING.CODDIAEGR =DIAG.CODDIAGNO INNER JOIN
		dbo.INPACIENT AS PAC WITH (NOLOCK) ON ING.IPCODPACI =PAC.IPCODPACI INNER JOIN
		dbo.INENTIDAD AS ENT WITH (NOLOCK) ON ING.CODENTIDA =ENT.CODENTIDA INNER JOIN
		dbo.ADCENATEN AS Cen on Cen.CODCENATE =ING.CODCENATE INNER JOIN
		dbo.INPROFSAL AS S ON S.CODPROSAL =hc.CODPROSAL INNER JOIN
		dbo.INUBICACI AS UBI ON PAC.AUUBICACI =UBI.AUUBICACI LEFT OUTER JOIN
		Contract .CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id LEFT OUTER JOIN
		dbo.ADGRUETNI AS GRE WITH (NOLOCK) ON PAC.CODGRUPOE =GRE.CODGRUPOE LEFT OUTER JOIN
		  (SELECT PA.ID, PA.IPCODPACI,PA.IDADPOBESPE FROM dbo.ADPOBESPEPAC AS PA INNER JOIN
			(SELECT MIN(ID) AS ID, IPCODPACI FROM dbo.ADPOBESPEPAC GROUP BY IPCODPACI ) AS G ON G.ID=PA.ID) AS G2 ON PAC.IPCODPACI =G2.IPCODPACI LEFT OUTER JOIN
		dbo.ADPOBESPE AS PE ON PE.ID =G2.IDADPOBESPE INNER JOIN
		dbo.HCEXFISIC AS EFIS ON ING.NUMINGRES =EFIS.NUMINGRES AND EFIS.IPCODPACI =ING.IPCODPACI AND HC.NUMEFOLIO =EFIS.NUMEFOLIO
		WHERE HC.TIPHISPAC ='I' AND HC.IDMODELOHC IS NOT NULL and CAST(HC.FECHISPAC AS date ) between @FechaInicial and @FechaFinal  and HC.IDMODELOHC = @IDMODELOHC
	
end


begin tran SubirVariablesHistorico
begin try

	declare @count as int = (select COUNT(ID) from @table )
	--SELECT * FROM  @table
	declare @contador as int = 1

	DECLARE @SQL NVARCHAR(MAX)=N''; 
	DECLARE @SQLCrearTabla NVARCHAR(MAX)=N''; 


	--odontologia
	IF @IDMODELOHC = 28 BEGIN	
		if exists (SELECT * FROM sysobjects  WHERE xtype='u' AND name = 'ESE_HC_HISTORICO_ODONTOLOGIA' ) begin
			drop table ESE_HC_HISTORICO_ODONTOLOGIA
		end
		WHILE @contador <= @count BEGIN	
			declare @IdtmpHC as int = (select IDHiSPACA from @table where ID = @contador )
			set @SQL =  (select Query  from dbo.fnListarVariablesDinamicas2(@IdtmpHC))
 			if @contador = 1 BEGIN
                SET @SQLCrearTabla = 'SELECT * INTO ESE_HC_HISTORICO_ODONTOLOGIA FROM ( ' + @SQL + ' ) as x'
				exec sp_executesql @SQLCrearTabla
			end else begin
				insert into ESE_HC_HISTORICO_ODONTOLOGIA 
				exec sp_executesql @SQL
			end		 
			set @contador = @contador + 1 
			print @contador
		END 
		commit tran SubirVariablesHistorico
		select * from ESE_HC_HISTORICO_ODONTOLOGIA
	END 

	--Higiene oral
	IF @IDMODELOHC = 33 BEGIN	
		if exists (SELECT * FROM sysobjects  WHERE xtype='u' AND name = 'ESE_HC_HISTORICO_HIGIENEORAL' ) begin
		drop table ESE_HC_HISTORICO_HIGIENEORAL
		end
		WHILE @contador <= @count BEGIN	
			set @IdtmpHC  = (select IDHiSPACA from @table where ID = @contador )
			set @SQL =  (select Query  from dbo.fnListarVariablesDinamicas2(@IdtmpHC))
 			if @contador = 1 begin
				set @SQLCrearTabla = 'SELECT * INTO ESE_HC_HISTORICO_HIGIENEORAL FROM ( ' + @SQL + ' ) as x'
				exec sp_executesql @SQLCrearTabla
			end else begin
				insert into ESE_HC_HISTORICO_HIGIENEORAL 
				exec sp_executesql @SQL
			end		 
			set @contador = @contador + 1 
			print @contador
		END 
		commit tran SubirVariablesHistorico
		select * from ESE_HC_HISTORICO_HIGIENEORAL
	END

		--Consulta de control por odontología
	IF @IDMODELOHC = 42 BEGIN	
		if exists (SELECT * FROM sysobjects  WHERE xtype='u' AND name = 'ESE_HC_HISTORICO_CONTROL_ODONTOLOGIA' ) begin
		drop table ESE_HC_HISTORICO_CONTROL_ODONTOLOGIA
		end
		WHILE @contador <= @count BEGIN	
			set @IdtmpHC  = (select IDHiSPACA from @table where ID = @contador )
			set @SQL =  (select Query  from dbo.fnListarVariablesDinamicas2(@IdtmpHC))
 			if @contador = 1 begin
				  --PRINT @SQL
				SET @SQLCrearTabla = 'SELECT * INTO ESE_HC_HISTORICO_CONTROL_ODONTOLOGIA FROM ( ' + @SQL + ' ) as x'
				exec sp_executesql @SQLCrearTabla
			end else begin
				insert into ESE_HC_HISTORICO_CONTROL_ODONTOLOGIA 
				exec sp_executesql @SQL
			end		 
			set @contador = @contador + 1 
			print @contador
		END 
		commit tran SubirVariablesHistorico
		select * from ESE_HC_HISTORICO_CONTROL_ODONTOLOGIA
	END



	--primero infancia
	IF @IDMODELOHC = 7 BEGIN	
		if exists (SELECT * FROM sysobjects  WHERE xtype='u' AND name = 'ESE_HC_HISTORICO_PRIMERAINFANCIA' ) begin
			drop table ESE_HC_HISTORICO_PRIMERAINFANCIA
		end
		WHILE @contador <= @count BEGIN	
			set @IdtmpHC  = (select IDHiSPACA from @table where ID = @contador )
			set @SQL =  (select Query  from dbo.fnListarVariablesDinamicas2(@IdtmpHC))
			if @contador = 1 BEGIN
            --PRINT @SQL
				set @SQLCrearTabla = 'SELECT * INTO ESE_HC_HISTORICO_PRIMERAINFANCIA FROM ( ' + @SQL + ' ) as x'
				PRINT @SQLCrearTabla
				EXEC sp_executesql @SQLCrearTabla
			end else begin
				insert into ESE_HC_HISTORICO_PRIMERAINFANCIA 
				exec sp_executesql @SQL
			end		 
			set @contador = @contador + 1 
			print @contador
		END 
		commit tran SubirVariablesHistorico
		select * from ESE_HC_HISTORICO_PRIMERAINFANCIA
	END


end try
begin catch
	rollback tran SubirVariablesHistorico
end catch

END