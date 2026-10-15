CREATE TABLE [PathologyALULA].[DIAGNOSIS_RESULTS] (
    [id]                    INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [order_id]              VARCHAR (16)   NOT NULL,
    [report_base64]         VARCHAR (MAX)  NOT NULL,
    [diagnosis_description] VARCHAR (5000) NULL,
    [diagnosis_date]        DATETIME       NULL,
    [diagnosis_type]        INT            NULL,
    [diagnosing_clinician]  VARCHAR (150)  NULL,
    [parent_diagnosis]      INT            NULL,
    CONSTRAINT [PK_DIAGNOSIS_RESULTS] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico de los padres', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'parent_diagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Médico que realiza el diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosing_clinician';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 reporte  2 adendum', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'diagnosis_description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte en formato base 64', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'report_base64';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del pedido', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'order_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'DIAGNOSIS_RESULTS', @level2type = N'COLUMN', @level2name = N'id';

