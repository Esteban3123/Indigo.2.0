CREATE TABLE [Payroll].[ElectronicPayrollDetail] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicPayrollId] INT           NOT NULL,
    [Destination]         TINYINT       NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [Status]              BIT           NOT NULL,
    [Response]            VARCHAR (MAX) NULL,
    [Comments]            VARCHAR (MAX) NULL,
    [ResponseData]        VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ElectronicPayrollDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayrollDetail_ElectronicPayroll] FOREIGN KEY ([ElectronicPayrollId]) REFERENCES [Payroll].[ElectronicPayroll] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos completos de la respuesta (VARCHAR MAX) recibida del servicio DIAN en formato estructurado; almacena la respuesta íntegra para auditoría y trazabilidad de envíos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacenamos la respuesta recibida del servicio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios o descripción (VARCHAR MAX) de la respuesta recibida del servicio DIAN; detalla mensajes de error, advertencias o notas sobre el resultado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios en la respuesta recibida', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de respuesta (VARCHAR MAX) recibido del servicio DIAN; contiene el código de estado o error retornado por la validación electrónica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Response';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la respuesta recibida', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Response';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Response';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del envío (BIT): 0=Fallido (respuesta válida de DIAN incumple requisitos), 1=Exitoso (respuesta válida cumple requisitos); refleja aceptación de la nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del envío      0. Fallido (Si se obtiene respuesta valida del servicio no cumple los requisitos)      1. Exitoso (Si se obtiene respuesta valida del servicio cumple los requisitos)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro e intento de envío a DIAN o validación; marca cuándo se intentó la transmisión.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro e intento de envío', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de destino del envío (TINYINT): 0=Validación previa XML, 1=Envío a DIAN, 2=Validación post-envío; indica en qué fase del flujo de transmisión se encuentra.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el destinato del envío      0. Validacion previa a la generacion del XML      1. Envio Documento Electronico a la DIAN      2. Validacion del Documento Enviado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del documento de nómina electrónica asociado; referencia a la nómina que se intenta enviar a DIAN o valida.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento electronico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del detalle del envío de nómina electrónica; clave primaria que rastrea cada intento de transmisión del documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del envío del documento electronico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de cada intento de envío de nómina electrónica, incluyendo el destino al que se transmitió, la respuesta obtenida y el estado del proceso para cada comprobante de nómina electrónica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollDetail';
