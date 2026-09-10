CREATE TABLE [dbo].[CHREGESTA] (
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NOT NULL,
    [CODICAMAS]   INT                                                                              NOT NULL,
    [FECINIEST]   DATETIME                                                                         NOT NULL,
    [FECFINEST]   DATETIME                                                                         NOT NULL,
    [CODTIPEST]   CHAR (3)                                                                         NOT NULL,
    [REINGRESO]   BIT                                                                              NOT NULL,
    [TIPINGEST]   CHAR (2)                                                                         NOT NULL,
    [TIPREIUNI]   BIT                                                                              NOT NULL,
    [REGESTADO]   INT                                                                              NOT NULL,
    [REGDIAEST]   INT                                                                              NULL,
    [REGUSUARI]   CHAR (20)                                                                        NULL,
    [FECREGSIS]   DATETIME                                                                         CONSTRAINT [DF_CHREGESTA_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [INDAUDFOR]   NUMERIC (18)                                                                     NOT NULL,
    [JUSCAMANT]   VARCHAR (4000)                                                                   NULL,
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENESTLIQ]   TINYINT                                                                          NULL,
    [NOREQAUTO]   BIT                                                                              NULL,
    [NOREQAUTJUS] VARCHAR (4000)                                                                   NULL,
    [IDAGEPROGQX] INT                                                                              NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODESPECI]   CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_CHREGESTA_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHREGESTA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHREGESTA_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHREGESTA_CHTIPESTA] FOREIGN KEY ([CODTIPEST]) REFERENCES [dbo].[CHTIPESTA] ([CODTIPEST]),
    CONSTRAINT [FK_CHREGESTA_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_CHREGESTA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHREGESTA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[CHREGESTA] NOCHECK CONSTRAINT [FK_CHREGESTA_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHREGESTA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHREGESTA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO



GO



GO



GO



GO



GO
ALTER TABLE [dbo].[CHREGESTA] NOCHECK CONSTRAINT [FK_CHREGESTA_INPROFSAL];


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA]
    ON [dbo].[CHREGESTA]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_CHREGESTA_7_1256391545__K3_K10_K2_K1]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [REGESTADO] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_CODICAMAS_FECINIEST_FECFINEST_CODTIPEST_INDAUDFOR_IPCODPACI_NUMINGRES_REGDIAEST_REGESTADO_REGUSUARI_REINGRESO]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [FECINIEST] ASC, [FECFINEST] ASC)
    INCLUDE([CODTIPEST], [INDAUDFOR], [IPCODPACI], [NUMINGRES], [REGDIAEST], [REGESTADO], [REGUSUARI], [REINGRESO], [TIPINGEST], [TIPREIUNI]);


GO
CREATE NONCLUSTERED INDEX [IDX_RegistroEstancia]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [REGESTADO] ASC)
    INCLUDE([CODTIPEST], [IPCODPACI], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_CODICAMAS_FECINIEST_FECFINEST_CODTIPEST]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [FECINIEST] ASC, [FECFINEST] ASC)
    INCLUDE([CODTIPEST], [ID], [INDAUDFOR], [IPCODPACI], [NUMINGRES], [REGDIAEST], [REGESTADO], [REGUSUARI], [REINGRESO], [TIPINGEST], [TIPREIUNI]);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_1]
    ON [dbo].[CHREGESTA]([NUMINGRES] ASC, [CODICAMAS] ASC, [FECINIEST] ASC, [CODTIPEST] ASC, [REGESTADO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NUMINGRESRIPS]
    ON [dbo].[CHREGESTA]([NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_REGESTADO_CODICAMAS_IPCODPACI_NUMINGRES]
    ON [dbo].[CHREGESTA]([REGESTADO] ASC)
    INCLUDE([CODICAMAS], [IPCODPACI], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_NUMINGRES]
    ON [dbo].[CHREGESTA]([NUMINGRES] ASC)
    INCLUDE([CODICAMAS], [FECFINEST]);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_CODICAMAS_FECINIEST_FECFINEST_NUMINGRES]
    ON [dbo].[CHREGESTA]([CODICAMAS] ASC, [FECINIEST] ASC)
    INCLUDE([FECFINEST], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_FECINIEST_Censo]
    ON [dbo].[CHREGESTA]([FECINIEST] ASC)
    INCLUDE([FECFINEST], [CODICAMAS], [IPCODPACI], [NUMINGRES], [CODTIPEST], [FECREGSIS], [REGUSUARI], [CODPROSAL], [CODESPECI]);


GO
CREATE TRIGGER [dbo].[tgg_ActualizarBedType_TypeTransfer]
   ON  [dbo].[CHREGESTA]
   AFTER INSERT, UPDATE
AS 
BEGIN

	declare @Paciente as varchar(25) = (select top 1 IPCODPACI   from inserted)
	declare @Ingreso as varchar(10) = (select top 1 NUMINGRES  from inserted)
	declare @Estado as integer = (select top 1 REGESTADO  from inserted)

SET NOCOUNT ON;


if @Estado = 2 begin 
	
	-- Insert the bed ids from the inserted or updated records in CHREGESTA into the temporary table
    CREATE TABLE #TablaEstanacia (CodigoCama INT);
	INSERT INTO #TablaEstanacia (CodigoCama)
    SELECT distinct A.CODICAMAS FROM CHREGESTA A INNER JOIN CHCAMASHO B ON A.CODICAMAS = B.CODICAMAS where B.ESTADCAMA IN (1,2) AND  IPCODPACI = @Paciente and NUMINGRES = @Ingreso


    -- Update the status in CHCAMASHO based on the bed ids from the temporary table
    UPDATE CHCAMASHO
    SET BedType = null, TypeTransfer = null 
    FROM CHCAMASHO c
    INNER JOIN #TablaEstanacia t ON c.CODICAMAS  = t.CodigoCama;

    -- Clean up the temporary table
    DROP TABLE #TablaEstanacia;
END

END
GO
DISABLE TRIGGER [dbo].[tgg_ActualizarBedType_TypeTransfer]
    ON [dbo].[CHREGESTA];


GO
Create trigger [dbo].[DetectarDobleRegistroHSAP]
on [dbo].[CHREGESTA]
for insert as
begin
    declare @Ingreso as varchar(10) = (select  NUMINGRES  from inserted)
	declare @Identificacion as varchar(10) = (select IPCODPACI    from inserted)
			
	   declare @ExisteDiagnosticoenIndiagnop int
			select @ExisteDiagnosticoenIndiagnop = count(*) from CHREGESTA where NUMINGRES = @Ingreso AND IPCODPACI   = @Identificacion  AND REGESTADO = 1

			if @ExisteDiagnosticoenIndiagnop > 1 begin
				  raiserror('Contacte administrador sistemas: no puede llegar Grabar dos registro activos :)!',10,1)
	              rollback transaction
			end
end
GO
DISABLE TRIGGER [dbo].[DetectarDobleRegistroHSAP]
    ON [dbo].[CHREGESTA];


GO
CREATE TRIGGER [dbo].[UPDATE_CODPROSAL] 
ON [dbo].[CHREGESTA] AFTER INSERT AS  

IF((SELECT COUNT(*) FROM INSERTED i where i.CODPROSAL IS NULL) > 0) BEGIN 	
	UPDATE CHREGESTA 
		SET CODPROSAL = '999', CODESPECI = '002'
	where ID = (SELECT ID FROM INSERTED)
END;
GO
CREATE TRIGGER trg_UpdateADIngreso
ON CHREGESTA
AFTER UPDATE
AS
BEGIN
    UPDATE ADINGRESO
    SET ADINGRESO.CODCAMACT = i.CODICAMAS
    FROM ADINGRESO b
    INNER JOIN INSERTED i ON b.NUMINGRES = i.NUMINGRES
    WHERE i.REGESTADO = 1 AND i.CODICAMAS <> b.CODCAMACT;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica tratante (ej: Cirugía, Medicina Interna, Pediatría). CHAR(3). Identifica la disciplina clínica responsable de la estancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Especialidad Tratante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud/médico tratante. VARCHAR(25), PII ofuscado. FK a INPROFSAL. Identifica al profesional responsable de la atención durante la estancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Médico Tratante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la agenda/programación de cirugía o procedimiento quirúrgico. INT. Referencia cruzada a tabla de agendas de procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Tabla donde se guardan las programaiones de cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación textual del por qué no se requiere autorización previa. VARCHAR(4000). Campo de argumentación clínica o administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NOREQAUTJUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NOREQAUTJUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NOREQAUTJUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: 1=No requiere autorización previa. TINYINT. Controla si la estancia necesita trámite de autorización o cobertura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NOREQAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Actualizar el No requiere autorización  1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NOREQAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NOREQAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación de estancia. TINYINT. Estados: 1=Sin Liquidar, 2=Liquidada Parcial, 3=Liquidada Total. Relacionado con RIPS y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'GENESTLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Liquidacion de Estancia VIE  1. Sin Liquidar  2. Liquidada Parcial  3. Liquidada Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'GENESTLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'GENESTLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de estancia. INT IDENTITY. Pk_CHREGESTA_1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación del cambio de cama o estancia en fechas anteriores. VARCHAR(4000). Motivo clínico o administrativo del cambio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'JUSCAMANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion cambio estancia fecha anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'JUSCAMANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'JUSCAMANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y conformidad. NUMERIC(18). Campo de trazabilidad para control interno y auditoría de registros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró el movimiento en el sistema. DATETIME. Timestamp de creación del registro, default getdate().', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que se Registra en el Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/operador que crea o modifica el registro de estancia. CHAR(20). Trazabilidad de quién realiza el ingreso en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que Crea el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de días de estancia (cálculo entre fecha inicial y final). INT. Usado para liquidación, indicadores de ocupación y cálculo de aranceles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGDIAEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Dias de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGDIAEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGDIAEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de estancia. INT. Valores: 1=Activo, 2=Pendiente Liquidar, 3=Liquidado. Controla ciclo de vida del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Registro:  1: Activo  2: Pendiente Liquidar  3: Liquidado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REGESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de reingreso a la unidad funcional: 1=Sí, reingresa a unidad. BIT. Identifica si el paciente vuelve a ingresar a la misma unidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'TIPREIUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Reingreso a la Unidad  True-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'TIPREIUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'TIPREIUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso a estancia. CHAR(2). NU=Nuevo Ingreso a Unidad, TC=Traslado Interno de Cama. Clasifica origen del movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'TIPINGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Ingreso Estancia:  NU -> Nuevo Unidad  TC -> Traslado Interno de Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'TIPINGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'TIPINGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: 1=Paciente reingresa a la IPS. BIT. Marca si es primer ingreso o reingreso en la institución de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reingreso de Paciente a la IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'REINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de estancia (cuidado intensivo, general, observación, etc.). CHAR(3), FK a CHTIPESTA. Clasifica nivel de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización/egreso de la estancia. DATETIME. Marca salida del paciente de la cama/unidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECFINEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECFINEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECFINEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio/ingreso a la estancia en cama. DATETIME. Marca entrada del paciente a la cama/unidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECINIEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECINIEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'FECINIEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la cama hospitalaria. INT, FK a CHCAMASHO. Ubicación física donde se hospitaliza el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso del paciente. CHAR(10), FK a ADINGRESO. Identifica el episodio de atención/hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del paciente (cédula, documento de identidad). VARCHAR(25), PII ofuscado. FK a INPACIENT. Núcleo de identificación del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro histórico de estados o estancias del paciente durante un ingreso hospitalario: cama asignada, fechas de inicio y fin del estado, tipo de estancia (urgencias, hospitalización, etc.), reingresos y cambios de unidad. Permite trazabilidad del recorrido del paciente dentro del centro asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTA';

GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTA_NUMINGRES_REGESTADO]
    ON [dbo].[CHREGESTA]([NUMINGRES] ASC, [REGESTADO] ASC);
