CREATE proc [dbo].[SP_HC_CapturarHistorialObstetrico]
  @Ingreso char(10)
, @Folio char(10)
, @Paciente varchar(25)
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
			a.ipcodpaci = @Paciente and c.fechispac <= (select fechispac from hchispaca where numingres = @Ingreso and numefolio = @Folio)
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el historial obstétrico de una paciente a lo largo de sus atenciones clínicas, calculando el IMC (peso en kg dividido por talla en metros al cuadrado) y las semanas de gestación registradas en cada folio de historia clínica. Combina datos de antecedentes ginecológicos y obstétricos (HCANTGINE), examen físico (HCEXFISIC), notas clínicas (HCHISPACA) y datos demográficos de la paciente (INPACIENT), filtrando únicamente los registros cuya fecha de atención sea posterior a la fecha probable de parto estimada más reciente. Se utiliza para el seguimiento y control prenatal de la paciente, permitiendo visualizar la evolución del IMC y las semanas de gestación en los controles realizados después de la fecha probable de parto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_CapturarHistorialObstetrico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_CapturarHistorialObstetrico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera indicadores obstétricos (IMC y semanas de gestación) del paciente registrados después de la fecha probable de parto del último control clínico previo o igual al ingreso/folio actual.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CapturarHistorialObstetrico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en hcantgine para el paciente indicado.; Debe existir un registro en hchispaca con el ingreso y folio indicados para obtener la fecha de referencia.; Peso y talla en hcexfisic deben ser numéricos y la talla distinta de 0 para evitar división por cero al calcular IMC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CapturarHistorialObstetrico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'IMC se calcula como (peso_g/1000) / ((talla_cm/100)^2), es decir kg/m².; Solo se consideran historiales clínicos del paciente con fecha menor o igual a la del folio/ingreso suministrado.; El umbral de filtrado final es la fecha probable de parto (fecpropar) del registro con fechispac más reciente dentro del subconjunto previo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CapturarHistorialObstetrico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes ginecoobstétricos; Historial obstétrico; IMC (Índice de Masa Corporal); Semanas de gestación; Fecha probable de parto; Examen físico del paciente; Ingreso/Folio clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CapturarHistorialObstetrico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve IMC y semanas de gestación de los controles cuya fechispac sea posterior a la fecha probable de parto del control más reciente (con fechispac <= fechispac del ingreso/folio recibido) del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CapturarHistorialObstetrico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcantgine; dbo.hcexfisic; dbo.hchispaca; dbo.inpacient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CapturarHistorialObstetrico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CapturarHistorialObstetrico';
-- GO
