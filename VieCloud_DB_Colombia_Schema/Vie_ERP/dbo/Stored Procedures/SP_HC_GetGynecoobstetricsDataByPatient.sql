CREATE proc [dbo].[SP_HC_GetGynecoobstetricsDataByPatient]
  @ingres char(10)
, @sheetnumber char(10)
, @patient varchar(25)
as
begin
	declare @recorddata as table (numingres varchar(20), numefolio varchar(20), ipsexopac int, fechispac datetime, imc decimal, nomsemges numeric, fecpropar datetime)
	insert into @recorddata
		select
			a.numingres
			,a.numefolio
			,d.ipsexopac
			,c.fechispac
			,(cast(b.pesopacie as decimal)/1000)/((cast(b.tallapaci as decimal)/ 100)*(cast(b.tallapaci as decimal)/ 100)) imc
			,a.nomsemges
			,a.fecpropar
		from
			hcantgine as a
			left join hcexfisic b on b.ipcodpaci = a.ipcodpaci and b.numingres = a.numingres and b.numefolio = a.numefolio
			left join hchispaca c on b.ipcodpaci = a.ipcodpaci and c.numingres = a.numingres and c.numefolio = a.numefolio
			left join inpacient d on d.ipcodpaci = a.ipcodpaci 
		where
			a.ipcodpaci = @patient and c.fechispac <= (select fechispac from hchispaca where numingres = @ingres and numefolio = @sheetnumber)
		order by c.fechispac

	declare @estimatedduedate datetime = (select top 1 fecpropar from @recorddata order by fechispac desc)

	select 
		 imc
		,nomsemges
	from
		@recorddata
	where
		fechispac > @estimatedduedate
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene los datos gineco-obstétricos históricos de una paciente a partir de su código de identificación, número de ingreso y número de folio. Combina los antecedentes ginecológicos y obstétricos (HCANTGINE), el examen físico (HCEXFISIC) para calcular el IMC (peso y talla), la historia clínica (HCHISPACA) para la fecha de la nota clínica, y los datos del paciente (INPACIENT) para el sexo. Determina la fecha probable de parto más reciente y devuelve únicamente los registros de IMC y semanas de gestación correspondientes a controles prenatales posteriores a esa fecha estimada de parto, permitiendo hacer seguimiento del embarazo vigente. Es útil para visualizar la evolución del peso materno y la edad gestacional durante el control prenatal actual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_GetGynecoobstetricsDataByPatient';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_GetGynecoobstetricsDataByPatient';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera el IMC y las semanas de gestación registradas posteriormente a la última fecha probable de parto del paciente, para soportar la historia clínica gineco-obstétrica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetGynecoobstetricsDataByPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en inpacient con peso y talla registrados en hcexfisic para poder calcular el IMC sin división por cero.; Debe existir un registro de hchispaca correspondiente al ingreso y folio recibidos como referencia temporal.; Deben existir antecedentes ginecológicos (hcantgine) asociados al paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetGynecoobstetricsDataByPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera registros gineco-obstétricos del paciente cuya fecha de historia (fechispac) sea menor o igual a la fecha del ingreso/folio de referencia.; El IMC se calcula como peso(kg)/talla(m)^2, convirtiendo peso de gramos a kilogramos (/1000) y talla de centímetros a metros (/100).; La fecha probable de parto utilizada como umbral es la del registro más reciente (top 1 por fechispac desc) dentro del histórico filtrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetGynecoobstetricsDataByPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes ginecológicos; Historia clínica gineco-obstétrica; Índice de masa corporal (IMC); Semanas de gestación; Fecha probable de parto; Examen físico del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetGynecoobstetricsDataByPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve IMC y semanas de gestación únicamente de los registros cuya fechispac es posterior a la última fecha probable de parto (fecpropar) ordenada por fechispac descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetGynecoobstetricsDataByPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcantgine; dbo.hcexfisic; dbo.hchispaca; dbo.inpacient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetGynecoobstetricsDataByPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetGynecoobstetricsDataByPatient';
-- GO
