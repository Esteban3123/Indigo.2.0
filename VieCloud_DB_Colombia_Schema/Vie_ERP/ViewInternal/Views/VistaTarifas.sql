

create view [ViewInternal].[VistaTarifas]
as
(
select
cg.Code as CodigoGrupoAtencion,
cg.Name as NombreGrupoAtencion,
cdr.InitialDate as FechaInicial,
cdr.EndDate as FechaFinal,
dr.Code as CodigoTarifa,
dr.Name as NombreTarifa,
dr.Description as DescripcionTarifa,
case drd.RuleType when 1 then 'Servicio IPS' when 2 then 'CUPS' when 3 then 'SubGrupos' when 4 then 'Grupos' when 5 then 'General' end as Regla,
case drd.ConditionType when 1 then 'Horario' when 2 then 'Especialidad' when 3 then 'Unidad Funcional' when 4 then 'Tipo Unidad' when 5 then 'Ninguna' end as Condicion,
case drd.ConditionType when 5 then rm.Name else rmd.Name end as ManualTarifario,
case drd.ConditionType when 5 then drd.RateVariation else drdc.RateVariation end as Variacion,
case drd.ConditionType when 5 then drd.SalesValue else drdc.SalesValue end as ValorFijo
from Contract.CareGroup cg
inner join Contract.CareGroupDefinitionRate cdr on cg.Id = cdr.CareGroupId
inner join Contract.DefinitionRate dr on dr.Id = cdr.DefinitionRateId
inner join Contract.DefinitionRateDetail drd on drd.DefinitionRateId = dr.Id
left join Contract.DefinitionRateDetailCondition drdc on drdc.DefinitionRateDetailId = drd.Id
left join Contract.RateManual rm on rm.Id = drd.RateManualId
left join Contract.RateManual rmd on rmd.Id = drdc.RateManualId
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Esta vista consolida la configuración tarifaria de los contratos de salud, aplanando la jerarquía entre grupos de atención, definiciones de tarifas y sus reglas de detalle con sus condiciones. Permite consultar, por grupo de atención y período de vigencia, qué esquema tarifario aplica, bajo qué regla (Servicio IPS, CUPS, Subgrupos, Grupos o General) y condición (Horario, Especialidad, Unidad Funcional, Tipo Unidad o Ninguna), junto con el manual tarifario, variación porcentual y valor fijo resultante. Está orientada a reporting y auditoría de tarifas contractuales.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone de forma consolidada las tarifas contractuales asociadas a grupos de atención, traduciendo códigos internos de reglas y condiciones a etiquetas legibles y eligiendo manual tarifario, variación y valor fijo según el tipo de condición.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de grupos de atención con tarifas asociadas en CareGroupDefinitionRate; Cada DefinitionRate debe tener al menos un DefinitionRateDetail para aparecer en la vista', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cuando la condición es ''Ninguna'' (5) los valores tarifarios provienen del detalle; en cualquier otra condición provienen de la condición específica; Sólo se incluyen tarifas vinculadas a un grupo de atención mediante CareGroupDefinitionRate (INNER JOIN); Las condiciones y manuales tarifarios son opcionales: la vista no los exige (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de atención; Tarifa contractual; Manual tarifario; Vigencia de tarifa; Regla de tarifa (Servicio IPS, CUPS, SubGrupos, Grupos, General); Condición tarifaria (Horario, Especialidad, Unidad Funcional, Tipo Unidad); Variación tarifaria; Valor fijo de venta', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewInternal.VistaTarifas: Devuelve una fila por cada combinación grupo de atención × tarifa × detalle de tarifa, incluyendo opcionalmente sus condiciones y manuales tarifarios.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si drd.RuleType IN (1..5) → Mapea el tipo de regla a etiqueta: 1=Servicio IPS, 2=CUPS, 3=SubGrupos, 4=Grupos, 5=General; si drd.ConditionType IN (1..5) → Mapea la condición a etiqueta: 1=Horario, 2=Especialidad, 3=Unidad Funcional, 4=Tipo Unidad, 5=Ninguna; si drd.ConditionType = 5 (Ninguna) → Toma manual tarifario, variación y valor fijo desde el detalle de tarifa (DefinitionRateDetail y su RateManual) else Toma manual tarifario, variación y valor fijo desde la condición del detalle (DefinitionRateDetailCondition y su RateManual)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.CareGroupDefinitionRate; Contract.DefinitionRate; Contract.DefinitionRateDetail; Contract.DefinitionRateDetailCondition; Contract.RateManual', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaTarifas';
GO
