CREATE TABLE [Payroll].[ElectronicPayroll] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentType]    TINYINT       NOT NULL,
    [Year]            INT           NOT NULL,
    [Consecutive]     INT           NOT NULL,
    [Month]           TINYINT       NOT NULL,
    [EmployeePartyId] INT           NOT NULL,
    [EntityName]      VARCHAR (250) NOT NULL,
    [EntityId]        INT           NOT NULL,
    [Prefix]          VARCHAR (5)   NULL,
    [DocumentNumber]  VARCHAR (20)  NOT NULL,
    [CUNE]            VARCHAR (500) NULL,
    [Status]          TINYINT       NOT NULL,
    [CreationDate]    DATETIME      NOT NULL,
    [ShippingDate]    DATETIME      NULL,
    [ValidationDate]  DATETIME      NULL,
    [FilePath]        VARCHAR (250) NOT NULL,
    [Retry]           INT           NOT NULL,
    [ZipKey]          VARCHAR (500) NULL,
    [ContractId]      INT           NULL,
    CONSTRAINT [PK_ElectronicPayroll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayroll_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_ElectronicPayroll_ThirdParty] FOREIGN KEY ([EmployeePartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO

-- =============================================
-- Indice UNICO FILTRADO sobre (DocumentType, DocumentNumber)
-- Garantiza unicidad a nivel de motor SQL Server entre registros activos (Status <> 0).
-- Status = 0 (Invalida) queda excluido para permitir reusar DocumentNumber tras descarte explicito.
--
-- Reemplaza al trigger trg_AfterInsertElectronicPayrollDocumentNumber, que tenia:
--   - Bug en INSERT multiple (solo validaba la ultima fila del lote).
-- Nota: requiere que no existan duplicados activos en la tabla. Si los hay se deben establecer con status = 0
-- =============================================
CREATE UNIQUE NONCLUSTERED INDEX [UX_ElectronicPayroll_DocumentTypeNumber]
ON [Payroll].[ElectronicPayroll] ([DocumentType], [DocumentNumber])
WHERE [Status] <> 0;
GO
CREATE NONCLUSTERED INDEX [IX_EPdtco]
    ON [Payroll].[ElectronicPayroll] ([DocumentType])
    INCLUDE ([Year], [Month], [EntityName], [EntityId]);
GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-08-18
-- Description:	Trigger para que genere automáticamente el consecutivo del documento electrónico de nomina
-- =============================================
CREATE TRIGGER [Payroll].[tggElectronicPayrollGenerateConsecutive]
   ON  [Payroll].[ElectronicPayroll]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON

	DECLARE @Rows INT = 1,
			@Id INT = 0

	WHILE @Rows > 0
	BEGIN
		SELECT TOP 1
			@Id = Id
		FROM INSERTED
		WHERE Id > @Id
		ORDER BY Id

		SET @Rows = @@ROWCOUNT
		IF @Rows = 0 
		BEGIN
			BREAK
		END

		--------------------------------------------

		UPDATE ep
			SET ep.Consecutive = ISNULL
			(
				(
					SELECT MAX(ed.Consecutive)
					FROM Payroll.ElectronicPayroll ed 
					WHERE ed.Year = ep.Year AND  ed.DocumentType = ep.DocumentType
				), 0
			) + 1
		FROM Payroll.ElectronicPayroll ep
		WHERE ep.Id = @Id
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato laboral asociado a la nómina electrónica (FK a Payroll.Contract)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave/código generado tras validaciones iniciales exitosas, cuando el documento entra a cola de validación ante DIAN', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ZipKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al número generado una vez concluida exitosamente las validaciones iniciales y los documentos pasan a la cola de validación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ZipKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ZipKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de reintentos de envío del documento electrónico a la DIAN', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de intentos de envío', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta completa del archivo físico donde se almacena la nómina electrónica (XML/ZIP)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruta donde se encuentra el archivo físico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que DIAN valida el documento, cambiando estado de Enviado a Válido o Inválido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ValidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de validación ante la DIAN (Fecha en que cambia del estado Enviado a Válido o Inválido)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ValidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ValidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de envío del documento a DIAN, transición de estado Registrado a Enviado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envío a la DIAN (Fecha en que cambia del estado Registrado a Enviado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de nómina electrónica en el sistema', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del documento ante DIAN (0=Inválida, 1=Registrada, 2=Enviada, 3=Validada, 4=Validación Fallida, 66=Reenvío Obligatorio, 88=Pendiente)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el estado actual del registro ante la DIAN      0. Invalida      1. Registrada      2. Enviada      3. Validada      4. Validacion Fallida      ----------------------------------------------      66. Reenvío Obligatorio      88. Pendiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Único Nacional de Nómina Electrónica asignado por DIAN tras validación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'CUNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Unico del Documento Electronico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'CUNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'CUNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del documento de nómina sin incluir prefijo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del documento sin Prefijo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo alfabético del documento de nómina electrónica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prefijo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad/empresa que genera el registro de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento que genero el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad/empresa generadora del documento de nómina electrónica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad desde la cual se genero el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/empleado en Common.ThirdParty (FK, cédula/identificación del empleado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EmployeePartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EmployeePartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'EmployeePartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes o período de reporte cubierto por la nómina (1-12 o período especial)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes o Periodo de reporte del documento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del documento electrónico dentro del año fiscal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del documento electrónico en la vigencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año fiscal de generación del documento de nómina electrónica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año del documento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento: 1=Soporte de Pago de Nómina Electrónica, 2=Nota de Ajuste, 3=Nómina Electrónica Reportada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de Documentos      1. Documento Soporte de Pago de Nómina Electrónica      2. Notas de Ajuste      3. Nómina Electrónica Reportada  ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de documento de nómina electrónica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento de nómina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de nóminas electrónicas generadas y transmitidas a la DIAN. Guarda el documento de nómina electrónica por empleado, período (año/mes), estado de envío, archivo generado y código CUNE asignado por la DIAN.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayroll';
