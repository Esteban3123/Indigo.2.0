

CREATE PROCEDURE [dbo].[SPREP_HC_Generales_InsumosPaciente]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10),
@ManejoExterno bit
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT 
	RTRIM(B.CODPRODUC) AS 'CODIGO DEL PRODUCTO',
	CANPEDPRO AS 'CANTIDAD PEDIDA DEL PRODUCTO',
	RTRIM(DESPRODUC) AS 'DESCRIPCION DEL PRODUCTO',
	A.CODCENATE,
	A.UFUCODIGO,
	A.IPCODPACI, 
	RTRIM(B.JUSTIINSU) AS 'JUSTIFICACION', 
	B.MANEJOEXTRA
                   
FROM 
	HCSOLINSC A WITH(NOLOCK)
	INNER JOIN HCSOLINSD B WITH(NOLOCK) ON A.CODCONCEC = B.CODCONCEC 
	INNER JOIN IHLISTPRO C WITH(NOLOCK) ON B.CODPRODUC = C.CODPRODUC
              
WHERE
	A.IPCODPACI = @CodigoPaciente 
	AND A.NUMINGRES = @NumeroIngreso
	AND NUMEFOLIO = @NumeroFolio
	AND ( B.MANEJOEXTRA = @ManejoExterno OR ( @ManejoExterno = 0 AND  B.MANEJOEXTRA IS NULL ) )

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene el listado de insumos y productos (medicamentos, dispositivos médicos, material quirúrgico) solicitados para un paciente específico durante un ingreso hospitalario. Recibe como parámetros la cédula del paciente, el número de ingreso, el número de folio de la historia clínica y un indicador de manejo externo, y combina las solicitudes de insumos (HCSOLINSC y HCSOLINSD) con el catálogo maestro de productos (IHLISTPRO) para devolver el código, descripción, cantidad pedida, justificación del insumo y si es de manejo externo. Se utiliza para imprimir o visualizar en la historia clínica los insumos requeridos por un paciente en un folio determinado, diferenciando entre insumos de manejo interno e insumos de manejo externo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_InsumosPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_InsumosPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los insumos solicitados a un paciente en un ingreso y folio específicos, filtrando por si su manejo es externo o interno.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una solicitud de insumos (cabecera y detalle) asociada al paciente, ingreso y folio indicados.; Los productos referenciados en el detalle deben existir en el listado de productos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las consultas usan WITH(NOLOCK), permitiendo lecturas sucias.; Un MANEJOEXTRA NULL se considera equivalente a manejo no externo (0).; Solo se retornan insumos cuyo producto exista en el catálogo de productos (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Folio; Solicitud de insumos; Producto; Justificación de insumo; Manejo externo de insumo; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCSOLINSC/HCSOLINSD/IHLISTPRO: Devuelve código, descripción, cantidad pedida, justificación y bandera de manejo extra de los insumos solicitados que cumplan paciente, ingreso y folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ManejoExterno = 0 y MANEJOEXTRA IS NULL → Incluye también los insumos cuyo indicador de manejo externo es nulo (se tratan como no externos) else Solo se incluyen insumos donde MANEJOEXTRA coincide exactamente con el parámetro recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCSOLINSC; dbo.HCSOLINSD; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InsumosPaciente';
-- GO
