

CREATE VIEW [Report].[ViewNIIFServiciosTotal] AS
WiTH cte_total_niif as
 (
 SELECT 
	[ID_COMPANY],
	[NIT ENTIDAD],
	[ENTIDAD],
	[CODIGO GRUPO],
	[GRUPO DE ATENCION],
	[INGRESO],
	[FECHA INGRESO],
	[FECHA EGRESO],
	[TIPO IDENTIFICACION],
	[IDENTIFICACION],
	[PACIENTE],
	[ESTADO FACTURA],
	[NRO FACTURA],
	[FECHA FACTURA],
	[TOTAL FACTURA],
	[VR TOTAL DESCUENTO],
	[VR TOTAL ENTIDAD],
	[VR TOTAL CUOTA RECUPERACION],
	[CAB_ORD_NRO],
	[CAB_ORD_FECHA_ORDEN],
	[DET_ORD_TIPO],
	[FECHA DE SERVICIO],
	[HORA DE SERVICIO],
	[DET_ORD_GRUPO FACTURACION],
	[DET_ORD_GRUPO],
	[DET_ORD_SUBGRUPO],
	[DET_ORD_PRESENTACION],
	[DET_ORD_CUPS],
	[DET_ORD_DESCRIPCION_CUPS],
	[DET_ORD_CODIGO_SERVICIO/PRODUCTO],
	[DET_ORD_DESCRIPCION_SERVICIO/PRODUCTO],
	[DET_ORDQX_SUBCODIGO],
	[DET_ORDQX_SUBNOMBRE],
	[DET VISUALIZA FACTURA],
	[DET_ORD_PAQUETE],
	[DET_ORD_INCLUIDO_EN_PAQUETE],
	[DET_ORD_CUPS_PAQUETE],
	[DET_ORD_DESCRIPCION_CUPS_PAQUETE],
	[DET_ORD_TIPO_LIQUIDACION],
	[DET_ORD_CUPS_INCLUIDO],
	[DET_ORD_DESCRIPCION_CUPS_INCLUIDO],
	[DET_ORD_CODIGO_UNIDAD_FUNCIONAL],
	[DET_ORD_UNIDAD_FUNCIONAL_PRESTO_SERVICIO],
	[DET_ORD_COD_CENTRO_COSTO],
	[DET_ORD_CENTRO_COSTO],
	[DET_ORD_IDENTIFICACION_PROFESIONAL],
	[DET_ORD_PROFESIONAL],
	[DET_ORD_ESPECIALIDAD],
	[DET_ORD_CANTIDAD],
	[DET_ORD_VALOR_SERVICIO],
	[DET_ORD_VALOR_UNITARIO],
	[DET_ORD_VALOR_TOTAL],
	[DET_ORD_TECNOLOGIA_PBS],
	[FECHA ALTA MEDICA],
	[FECHA ANULACION],
	[CAMA ACTIVA],
	[ID_ORDE_DETALLE],
	[ID_FACTURA],
	[ID ENTIDAD],
	[TIPO REGISTRO],
	[FECHA SERVICIO BUSQUEDA],
	[FACTURADO],
	[AUTORIZACION SERVICIO],
	[AUTORIZACION],
	[NUMINGRES],
	[AMBITO DE INGRESO],
	[UNIDAD FUNCIONAL DE INGRESO],
	[UNIDAD FUNCIONAL DE EGRESO],
	[AMBITO DE EGRESO]
 FROM Report .ViewNIIFServiciosFacturados 
 UNION ALL
 SELECT 
 [ID_COMPANY],
	[NIT ENTIDAD],
	[ENTIDAD],
	[CODIGO GRUPO],
	[GRUPO DE ATENCION],
	[INGRESO],
	[FECHA INGRESO],
	[FECHA EGRESO],
	[TIPO IDENTIFICACION],
	[IDENTIFICACION],
	[PACIENTE],
	[ESTADO FACTURA],
	[NRO FACTURA],
	[FECHA FACTURA],
	[TOTAL FACTURA],
	[VR TOTAL DESCUENTO],
	[VR TOTAL ENTIDAD],
	[VR TOTAL CUOTA RECUPERACION],
	[CAB_ORD_NRO],
	[FECHA DE SERVICIO] AS [CAB_ORD_FECHA_ORDEN],
	[DET_ORD_TIPO],
	[FECHA DE SERVICIO],
	[HORA DE SERVICIO],
	[DET_ORD_GRUPO FACTURACION],
	[DET_ORD_GRUPO],
	[DET_ORD_SUBGRUPO],
	[DET_ORD_PRESENTACION],
	[DET_ORD_CUPS],
	[DET_ORD_DESCRIPCION_CUPS],
	[DET_ORD_CODIGO_SERVICIO/PRODUCTO],
	[DET_ORD_DESCRIPCION_SERVICIO/PRODUCTO],
	[DET_ORDQX_SUBCODIGO],
	[DET_ORDQX_SUBNOMBRE],
	[DET VISUALIZA FACTURA],
	[DET_ORD_PAQUETE],
	[DET_ORD_INCLUIDO_EN_PAQUETE],
	[DET_ORD_CUPS_PAQUETE],
	[DET_ORD_DESCRIPCION_CUPS_PAQUETE],
	[DET_ORD_TIPO_LIQUIDACION],
	[DET_ORD_CUPS_INCLUIDO],
	[DET_ORD_DESCRIPCION_CUPS_INCLUIDO],
	[DET_ORD_CODIGO_UNIDAD_FUNCIONAL],
	[DET_ORD_UNIDAD_FUNCIONAL_PRESTO_SERVICIO],
	[DET_ORD_COD_CENTRO_COSTO],
	[DET_ORD_CENTRO_COSTO],
	[DET_ORD_IDENTIFICACION_PROFESIONAL],
	[DET_ORD_PROFESIONAL],
	[DET_ORD_ESPECIALIDAD],
	[DET_ORD_CANTIDAD],
	[DET_ORD_VALOR_SERVICIO],
	[DET_ORD_VALOR_UNITARIO],
	[DET_ORD_VALOR_TOTAL],
	[DET_ORD_TECNOLOGIA_PBS],
	[FECHA ALTA MEDICA],
	[FECHA ANULACION],
	[CAMA ACTIVA],
	[ID_ORDE_DETALLE],
	[ID_FACTURA],
	[ID ENTIDAD],
	[TIPO REGISTRO],
	[FECHA SERVICIO BUSQUEDA],
	[FACTURADO],
	[AUTORIZACION SERVICIO],
	[AUTORIZACION],
	[NUMINGRES],
	[AMBITO DE INGRESO],
	[UNIDAD FUNCIONAL DE INGRESO],
	[UNIDAD FUNCIONAL DE EGRESO],
	[AMBITO DE EGRESO]
 FROM Report .ViewNIIFServiciosPendientes 
 )
SELECT
 *,
  CAST(nf.[FECHA SERVICIO BUSQUEDA] AS date) AS 'FECHA BUSQUEDA',
  YEAR(nf.[FECHA SERVICIO BUSQUEDA] ) AS 'AÑO BUSQUEDA',
  MONTH(nf.[FECHA SERVICIO BUSQUEDA]) AS 'MES BUSQUEDA',
  CONCAT(FORMAT(MONTH(nf.[FECHA SERVICIO BUSQUEDA]), '00') ,' - ', 
	   CASE MONTH(nf.[FECHA SERVICIO BUSQUEDA]) 
	    WHEN 1 THEN 'ENERO'
   	    WHEN 2 THEN 'FEBRERO'
	    WHEN 3 THEN 'MARZO'
	    WHEN 4 THEN 'ABRIL'
	    WHEN 5 THEN 'MAYO'
	    WHEN 6 THEN 'JUNIO'
	    WHEN 7 THEN 'JULIO'
	    WHEN 8 THEN 'AGOSTO'
	    WHEN 9 THEN 'SEPTIEMBRE'
	    WHEN 10 THEN 'OCTUBRE'
	    WHEN 11 THEN 'NOVIEMBRE'
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM
 cte_total_niif nf
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consolida mediante `UNION ALL` los servicios facturados (`ViewNIIFServiciosFacturados`) y los servicios pendientes de facturación (`ViewNIIFServiciosPendientes`) en un único conjunto de datos para reportes NIIF. Agrega columnas derivadas de la fecha de servicio (fecha, año, mes numérico y nombre del mes en español) y un sello de última actualización en zona horaria de Pakistán. Está orientada a consumo por herramientas de reporting financiero-clínico que requieren visibilidad total de servicios, facturados o no.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolidar en un único conjunto los servicios NIIF facturados y los pendientes de facturar, agregando columnas calculadas de fecha (año, mes, nombre del mes) y un timestamp de última actualización para análisis y reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las vistas Report.ViewNIIFServiciosFacturados y Report.ViewNIIFServiciosPendientes deben existir y exponer exactamente la misma estructura de columnas en el mismo orden.; La columna [FECHA SERVICIO BUSQUEDA] debe ser convertible a tipo date para poder derivar año, mes y nombre del mes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El conjunto resultante es la unión sin deduplicación (UNION ALL) de servicios facturados y pendientes, por lo que un mismo servicio podría aparecer si existiera en ambas vistas fuente.; Para los registros provenientes de ViewNIIFServiciosPendientes, la fecha de la orden (CAB_ORD_FECHA_ORDEN) se asume igual a la fecha del servicio, dado que no hay orden facturada asociada.; La marca de tiempo ULT_ACTUAL se calcula siempre con GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.; Los campos derivados de fecha (FECHA BUSQUEDA, AÑO/MES/MES NOMBRE BUSQUEDA) se calculan exclusivamente sobre [FECHA SERVICIO BUSQUEDA].', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicios facturados; Servicios pendientes de facturar; NIIF; Factura; Orden de servicio; Paciente; Entidad responsable de pago; Autorización; Ingreso/Egreso hospitalario; CUPS; Unidad funcional; Centro de costo; Cuota de recuperación', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewNIIFServiciosTotal: Devuelve la unión (UNION ALL) de Report.ViewNIIFServiciosFacturados y Report.ViewNIIFServiciosPendientes; en la rama de pendientes, [CAB_ORD_FECHA_ORDEN] se sustituye por [FECHA DE SERVICIO].; [RETURN_RESULT] Report.ViewNIIFServiciosTotal: Agrega columnas calculadas: FECHA BUSQUEDA = CAST([FECHA SERVICIO BUSQUEDA] AS date), AÑO BUSQUEDA = YEAR(...), MES BUSQUEDA = MONTH(...), MES NOMBRE BUSQUEDA = ''<MM> - <NOMBRE_MES_ES>''.; [RETURN_RESULT] Report.ViewNIIFServiciosTotal: Agrega ULT_ACTUAL = CONVERT(DATETIME, GETDATE() AT TIME ZONE ''Pakistan Standard Time'', 1), expresando la fecha/hora actual del servidor en zona horaria de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MONTH([FECHA SERVICIO BUSQUEDA]) entre 1 y 12 → Se traduce el número de mes a su nombre en español (ENERO..DICIEMBRE) concatenado con el mes formateado a dos dígitos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Report.ViewNIIFServiciosFacturados; Report.ViewNIIFServiciosPendientes', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosTotal';
GO
