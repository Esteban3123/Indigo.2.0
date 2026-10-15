CREATE TABLE [dbo].[HCORDPROQ] (
    [IDETIPHIS]                     CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]                     DATETIME                                                                         NOT NULL,
    [CODSERIPS]                     CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                     INT                                                                              NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000)                                                                   NULL,
    [PRISERIPS]                     CHAR (1)                                                                         NOT NULL,
    [ESTSERIPS]                     CHAR (1)                                                                         NULL,
    [MANEXTPRO]                     BIT                                                                              NOT NULL,
    [CODDIAGNO]                     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                     NOT NULL,
    [LATERALIDAD]                   TINYINT                                                                          NULL,
    [SOLICITASALA]                  BIT                                                                              NULL,
    [GENSERVICEORDER]               INT                                                                              NULL,
    [AUTO]                          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OTROSMATERIALES]               VARCHAR (5000)                                                                   NULL,
    [SOLICITAMATOST]                TINYINT                                                                          NULL,
    [VIEPSUPPLIER]                  INT                                                                              NULL,
    [AUTOCITA]                      INT                                                                              NULL,
    [CODANULA]                      CHAR (3)                                                                         NULL,
    [FECHAANULA]                    DATETIME                                                                         NULL,
    [PROFESIONALANULA]              CHAR (20)                                                                        NULL,
    [MOTIVOANULA]                   VARCHAR (1000)                                                                   NULL,
    [TIPOFACTURACION]               INT                                                                              NULL,
    [IDRIASCUPS]                    INT                                                                              NULL,
    [CODALMACEN]                    NVARCHAR (10)                                                                    NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                              NULL,
    [TraceabilityPaperworkEventsId] INT                                                                              NULL,
    [TraceabilityPaperworkId]       INT                                                                              NULL,
    [IDADRADICACIONQX]              INT                                                                              NULL,
    [OBSANULADO]                    VARCHAR (300)                                                                    NULL,
    [PROANULADO]                    CHAR (20)                                                                        NULL,
    [FECHAANULADO]                  DATETIME                                                                         NULL,
    [IDAREAPRO]                     INT                                                                              NULL,
    [CODCENATEPRO]                  CHAR (10)                                                                        NULL,
    [UFUCODIGOPRO]                  CHAR (10)                                                                        NULL,
    [CODUSUPRO]                     CHAR (20)                                                                        NULL,
    [FECHAPRO]                      DATETIME                                                                         NULL,
    [MEDREALI]                      CHAR (20)                                                                        NULL,
    [ESPEREALI]                     CHAR (3)                                                                         NULL,
    [CODMOTANU]                     CHAR (4)                                                                         NULL,
    CONSTRAINT [PK_HCORDPROQ] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDPROQ_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDPROQ_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPROQ_Cirugia] FOREIGN KEY ([AUTOCITA]) REFERENCES [dbo].[AGEPROGQX] ([CODAUTONU]),
    CONSTRAINT [FK_HCORDPROQ_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]) ON UPDATE CASCADE,
    CONSTRAINT [FK_HCORDPROQ_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDPROQ_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDPROQ_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDPROQ_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDPROQ_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID])
);


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_Cirugia];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INPacient];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_RIASCUPS];


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPROQ_CODSERIPS_ESTSERIPS_SOLICITASALA_FECORDMED]
    ON [dbo].[HCORDPROQ]([CODSERIPS] ASC, [ESTSERIPS] ASC, [SOLICITASALA] ASC, [FECORDMED] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [CODPROSAL], [OBSSERIPS], [PRISERIPS], [CODDIAGNO], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPROQ_NUMINGRES_MANEXTPRO]
    ON [dbo].[HCORDPROQ]([NUMINGRES] ASC, [MANEXTPRO] ASC)
    INCLUDE([CANSERIPS], [CODSERIPS], [GENSERVICEORDER], [IPCODPACI], [NUMEFOLIO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPROQ]
    ON [dbo].[HCORDPROQ]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC, [CODSERIPS] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPROQ_CODPROSAL_MANEXTPRO_ESTSERIPS]
    ON [dbo].[HCORDPROQ]([CODPROSAL] ASC, [MANEXTPRO] ASC, [ESTSERIPS] ASC)
    INCLUDE([AUTO], [CANSERIPS], [CODSERIPS], [FECORDMED], [GENSERVICEORDER], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [OBSSERIPS], [UFUCODIGO]);


GO

CREATE TRIGGER [dbo].[HospitalCima_Softland_SurgicalProcedureOrder] 
   ON [dbo].[HCORDPROQ]
   AFTER  INSERT,DELETE,UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

	
	DECLARE @action as  int 
	declare @dataid as varchar(200)

	if exists(select AUTO from inserted ) and exists(select AUTO from deleted) begin
		set @action = 2 --actualizando
		set @dataid = (select top 1 AUTO from inserted)
	end else if exists(select AUTO from inserted ) and not exists(select AUTO from deleted) begin
		set @action = 1 --insertando
		set @dataid = (select top 1 AUTO from inserted)
	end else if not exists(select AUTO from inserted ) and exists(select AUTO from deleted) begin
		set @action = 3 --eliminando
		set @dataid = (select top 1 AUTO from deleted)
	end

	
	
	if @dataid is null begin
		return
	end 

	insert into [integrations].[cimahospital_softland_synch]
			   ([dataid]
			   ,[type]
			   ,[action]
			   ,[transactiondate]
			   ,[state]
			   ,[errormessage])
		 values
			   (@dataid,6,@action,GETDATE(),0,NULL)

END

-------------------------------------------------------------------------------------------------------------------
/****** Object:  Trigger [dbo].[HospitalCima_Softland_NotSurgicalProcedureOrder]    Script Date: 8/24/2021 8:37:34 AM ******/
SET ANSI_NULLS ON
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del motivo de anulacion (codigo de la tabla HCMOANULB) -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especialidad del medico que realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'ESPEREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medico que realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'MEDREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha real en que procesa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'FECHAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario procesa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODUSUPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unidad Funcional desde el que se procesa. Este dato se genera desde el dashboard otros procedimientos cuando se procese', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'UFUCODIGOPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion desde el que se procesa. Este dato se genera desde el dashboard otros procedimientos cuando se procese', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODCENATEPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del area de otro procedimiento que procesa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'IDAREAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha militar, que se llena cuando se anula el servicio desde dashboard de otros procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'FECHAANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional de la salud, que se llena cuando se anula el servicio desde dashboard de otros procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'PROANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion cuando se anula el servicio desde dashboard de otros procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'OBSANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID radicacion de cirugia con la que se tramita el proceso de autorizacion (primer pestaña de Grupos de procedimientos invasivos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'IDADRADICACIONQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que indica el codigo de almacen asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODALMACEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'MOTIVOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'PROFESIONALANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'FECHAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auto incrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'AUTOCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor de VIE o de DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'VIEPSUPPLIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si en el procedimiento qx se solicitó material de osteosintesis  0-> No solicitó  1-> Solicitado  2-> Entregado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'SOLICITAMATOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Texto para escribir otros materiales de osteosintesis que se solicitaron', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'OTROSMATERIALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auto incrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Qx, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si este servicio solicitado exige sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'SOLICITASALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En caso de que aplique, especifica la lateralidad de la extremidad o parte del cuerpo del procedimiento a realizar    0->No Aplica  1->Izquierda  3->Derecha  4->Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Sala Programada  3: Cancelado  4: Resultado Revisado  5: Anulado  6: Programado no realizado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado:   1: Emergencia   2: Urgencia   3: Normal   4: Definir Conducta ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';

