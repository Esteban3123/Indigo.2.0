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