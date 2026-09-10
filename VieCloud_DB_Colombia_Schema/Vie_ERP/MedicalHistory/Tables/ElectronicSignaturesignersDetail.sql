CREATE TABLE [MedicalHistory].[ElectronicSignaturesignersDetail] (
    [Id]                           INT          IDENTITY (1, 1) NOT NULL,
    [IdHCDOCUMAD]                  NUMERIC (18) NULL,
    [IdElectronicSignaturesigners] INT          NOT NULL,
    [TokenDocument]                VARCHAR (40) NULL,
    [TokenSigner]                  VARCHAR (40) NULL,
    [Notified]                     BIT          DEFAULT ((0)) NOT NULL,
    [CreateDate]                   DATETIME     DEFAULT ('1900-01-01 00:00:00') NULL,
    CONSTRAINT [PK_ElectronicSignaturesignersDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicSignaturesigners_IdElectronicSignaturesigners] FOREIGN KEY ([IdElectronicSignaturesigners]) REFERENCES [MedicalHistory].[ElectronicSignatureSigners] ([Id]),
    CONSTRAINT [FK_HCDOCUMAD_IdHCDOCUMAD] FOREIGN KEY ([IdHCDOCUMAD]) REFERENCES [dbo].[HCDOCUMAD] ([CONSECUTI])
);


GO
ALTER TABLE [MedicalHistory].[ElectronicSignaturesignersDetail] NOCHECK CONSTRAINT [FK_HCDOCUMAD_IdHCDOCUMAD];




GO



GO
ALTER TABLE [MedicalHistory].[ElectronicSignaturesignersDetail] NOCHECK CONSTRAINT [FK_HCDOCUMAD_IdHCDOCUMAD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token único del firmante/profesional de la salud que firma electrónicamente el documento. Identificador de sesión/credencial generado por el servicio de firma digital (ej: Zapsign) para validar la identidad del signatario.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'TokenSigner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Token del firmante del documento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'TokenSigner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'TokenSigner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token único del documento creado en Zapsign. Referencia de integración con servicio externo de firma electrónica para rastrear y validar el documento firmado digitalmente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'TokenDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Token del documento creado en zapsign', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'TokenDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'TokenDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo (FK) que referencia el registro del firmante en la tabla ElectronicSignatureSigners. Vincula cada detalle de firma con el profesional/signatario responsable.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'IdElectronicSignaturesigners';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id consecutivo de la tabla de firmantes  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'IdElectronicSignaturesigners';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'IdElectronicSignaturesigners';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo (FK) que referencia el documento médico en la tabla HCDOCUMAD. Vincula cada firma electrónica con el documento clínico, receta, diagnóstico, procedimiento o acto médico firmado.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'IdHCDOCUMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutvio de la tabla de documentos.  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'IdHCDOCUMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'IdHCDOCUMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) consecutivo de la tabla ElectronicSignaturesignersDetail. Clave principal que registra cada evento/detalle de firma electrónica asociado a un documento y firmante.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de firmantes en el proceso de firma electrónica de documentos de historia clínica. Registra cada firmante asignado a un documento, con sus tokens de validación y el estado de notificación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el firmante ya fue notificado para firmar el documento (0 = no notificado, 1 = notificado).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'Notified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'Notified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró el firmante en el proceso de firma electrónica del documento.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'CreateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'CreateDate';
