create view [dbo].[ViewInvMedicamentoMasUtilizados]
as
select top 100 percent
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
'Medicamentos' 'TIPO',
count(d.CODPRODUC) 'Cantidad formulada', 
d.CODPRODUC 'Codigo Medicamento',
pro.DESPRODUC 'Descripcion Medicamento',
case PRO.TIPFORMED when 1 then '1-Peso' 
				   when 2 then '2-Volumen' 
				   when 3 then '3-Peso y Volumen' 
				   when 4 then '4-Unidad de Administración' end 'Tipo Formulacion',
PRO.PESTOTMED Peso,
pro.CODUNIPES 'Codigo Unidad Peso',
puni.DESUNIMED unidad_peso,
PRO.VOLTOTMED volumen,
CODUNIVOL 'Codigo Unidad volumne',
vuni.DESUNIMED unidad_volumen,
CODUNIADM 'Codigo Unidad Administracion',
auni.DESUNIMED unidad_admin,
form.CODFORMED 'Codigo Forma',
form.DESFORMED 'Nombre Forma',
case pro.PROCONTRO when 1 then 'SI' else 'NO' end as 'Control',
case pro.NOPOSPROD when 1 then 'NO' else 'SI' end as 'PBS',
1 as 'CANTIDAD',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from 
dbo.HCPRESCRD as d inner join
dbo.IHLISTPRO as pro on pro.CODPRODUC =d.CODPRODUC inner join
dbo.IHFORMEDI as form on form.CODFORMED =pro.CODFORMED inner join
Inventory.atc as atc on atc.Code =pro.CODPRODUC inner join
inventory.PharmaceuticalForm as ph on ph.Id =atc.PharmaceuticalFormId left join
DBO.INUNIMEDI AS puni on puni.CODUNIMED =pro.CODUNIPES left join
DBO.INUNIMEDI AS vuni on vuni.CODUNIMED =pro.CODUNIVOL left join
DBO.INUNIMEDI AS auni on auni.CODUNIMED =pro.CODUNIADM
group by 
d.CODPRODUC,pro.DESPRODUC,PRO.TIPFORMED,PRO.PESTOTMED ,puni.DESUNIMED ,PRO.VOLTOTMED,vuni.DESUNIMED,auni.DESUNIMED,form.CODFORMED,form.DESFORMED,atc.PharmaceuticalFormId,
ph.Name ,ph.RequireStability,pro.TIEESTMED,pro.CALCANAUT ,atc.StabilityMinimumHours ,atc.StabilityMaximumHours,pro.CODUNIPES,CODUNIADM,CODUNIVOL,
pro.PROCONTRO ,pro.NOPOSPROD

UNION

select top 100 percent
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
'mezcla' 'TIPO',
count(d.CODPRODUC) 'Cantidad formulada', 
d.CODPRODUC 'Codigo Medicamento',
pro.DESPRODUC 'Descripcion Medicamento',
case PRO.TIPFORMED when 1 then '1-Peso' 
				   when 2 then '2-Volumen' 
				   when 3 then '3-Peso y Volumen' 
				   when 4 then '4-Unidad de Administración' end 'Tipo Formulacion',
PRO.PESTOTMED Peso,
pro.CODUNIPES 'Codigo Unidad Peso',
puni.DESUNIMED unidad_peso,
PRO.VOLTOTMED volumen,
CODUNIVOL 'Codigo Unidad volumne',
vuni.DESUNIMED unidad_volumen,
CODUNIADM 'Codigo Unidad Administracion',
auni.DESUNIMED unidad_admin,
form.CODFORMED 'Codigo Forma',
form.DESFORMED 'Nombre Forma',
case pro.PROCONTRO when 1 then 'SI' else 'NO' end as 'Control',
case pro.NOPOSPROD when 1 then 'NO' else 'SI' end as 'PBS',
1 as 'CANTIDAD',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from 
dbo.HCINFCONC as d inner join
dbo.IHLISTPRO as pro on pro.CODPRODUC =d.CODPRODUC inner join
dbo.IHFORMEDI as form on form.CODFORMED =pro.CODFORMED inner join
Inventory .atc as atc on atc.Code =pro.CODPRODUC inner join
inventory.PharmaceuticalForm as ph on ph.Id =atc.PharmaceuticalFormId left join
DBO.INUNIMEDI AS puni on puni.CODUNIMED =pro.CODUNIPES left join
DBO.INUNIMEDI AS vuni on vuni.CODUNIMED =pro.CODUNIVOL left join
DBO.INUNIMEDI AS auni on auni.CODUNIMED =pro.CODUNIADM
group by 
d.CODPRODUC,pro.DESPRODUC,PRO.TIPFORMED,PRO.PESTOTMED ,puni.DESUNIMED ,PRO.VOLTOTMED,vuni.DESUNIMED,auni.DESUNIMED,form.CODFORMED,form.DESFORMED,atc.PharmaceuticalFormId,
ph.Name ,ph.RequireStability,pro.TIEESTMED,pro.CALCANAUT ,atc.StabilityMinimumHours ,atc.StabilityMaximumHours,pro.CODUNIPES,CODUNIADM,CODUNIVOL,
pro.PROCONTRO ,pro.NOPOSPROD
order by TIPO,count(d.CODPRODUC) desc
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ranking de medicamentos e insumos más formulados (prescritos y en mezclas magistrales) en la historia clínica, consolidando las prescripciones individuales (HCPRESCRD) y las mezclas intravenosas (HCINFCONC) con el catálogo de productos farmacéuticos (IHLISTPRO), la clasificación ATC, la forma de administración y las unidades de medida (peso, volumen y unidad de administración). Para cada medicamento muestra su código, descripción, tipo y valores de formulación (peso, volumen, unidad de administración), forma farmacéutica, si es medicamento de control especial y si pertenece al plan básico de salud (PBS). Sirve para análisis de consumo, gestión de inventario farmacéutico y reportería de medicamentos más utilizados en la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewInvMedicamentoMasUtilizados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewInvMedicamentoMasUtilizados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los medicamentos y mezclas más utilizados consolidando el conteo de prescripciones e información de concentraciones, con sus atributos farmacéuticos (forma, peso, volumen, unidades, control, PBS).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInvMedicamentoMasUtilizados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos consultados deben existir en el catálogo IHLISTPRO y tener forma farmacéutica en IHFORMEDI; Los productos deben estar clasificados en Inventory.atc con una PharmaceuticalForm asociada (INNER JOIN obligatorio); Las unidades de peso, volumen y administración pueden faltar (LEFT JOIN sobre INUNIMEDI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInvMedicamentoMasUtilizados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo incluye productos que estén catalogados en Inventory.atc y con PharmaceuticalForm (excluye productos no codificados en ATC); El identificador de compañía corresponde al nombre de la base de datos actual truncado a 9 caracteres; La marca de tiempo de actualización se calcula con la zona horaria ''Pakistan Standard Time''; El campo CANTIDAD siempre se reporta como 1 (constante); El conteo de uso se calcula agrupando por producto y sus atributos farmacéuticos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInvMedicamentoMasUtilizados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Mezcla / preparación magistral; Forma farmacéutica; Prescripción; Medicamento de control; Plan de Beneficios en Salud (PBS); Unidad de medida (peso, volumen, administración); Clasificación ATC; Estabilidad farmacéutica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInvMedicamentoMasUtilizados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve el conteo de usos por producto separado en dos categorías: ''Medicamentos'' (origen HCPRESCRD) y ''mezcla'' (origen HCINFCONC), unidos vía UNION y ordenados por TIPO y cantidad descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInvMedicamentoMasUtilizados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRO.TIPFORMED = 1/2/3/4 → Etiqueta el tipo de formulación como ''1-Peso'', ''2-Volumen'', ''3-Peso y Volumen'' o ''4-Unidad de Administración''; si pro.PROCONTRO = 1 → Marca el medicamento como controlado (''SI'') else Marca como no controlado (''NO''); si pro.NOPOSPROD = 1 → Marca PBS = ''NO'' (no incluido en plan de beneficios) else Marca PBS = ''SI''; si Origen del registro en HCPRESCRD → Clasifica el TIPO como ''Medicamentos'' else Si proviene de HCINFCONC clasifica como ''mezcla''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInvMedicamentoMasUtilizados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRD; dbo.HCINFCONC; dbo.IHLISTPRO; dbo.IHFORMEDI; Inventory.atc; Inventory.PharmaceuticalForm; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInvMedicamentoMasUtilizados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInvMedicamentoMasUtilizados';
GO
