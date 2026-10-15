CREATE TABLE [dbo].[HCORDPARAMLABD] (
    [ID]              INT                                                                        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORDPARAMLABID] INT                                                                        NOT NULL,
    [HCPARAMBID]      INT                                                                        NOT NULL,
    [RESULTADO]       VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)') NULL,
    [RESULTADOID]     INT                                                                        NULL,
    CONSTRAINT [PK_HCORDPARAMLABD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDPARAMLABD_HCORDPARAMLAB] FOREIGN KEY ([HCORDPARAMLABID]) REFERENCES [dbo].[HCORDPARAMLAB] ([ID]),
    CONSTRAINT [FK_HCORDPARAMLABD_HCPARAMB] FOREIGN KEY ([HCPARAMBID]) REFERENCES [dbo].[HCPARAMB] ([ID])
);


GO
ALTER TABLE [dbo].[HCORDPARAMLABD] NOCHECK CONSTRAINT [FK_HCORDPARAMLABD_HCORDPARAMLAB];


GO
ALTER TABLE [dbo].[HCORDPARAMLABD] NOCHECK CONSTRAINT [FK_HCORDPARAMLABD_HCPARAMB];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para almecenar el ID de la Relacion. Aplica para Valores Predefinidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'RESULTADOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado Paraclínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion con la tabla de configuracion de Paraclinicos Ambulatorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Relacion con Paraclínico Labioratorio HCORDPARAMLAB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'HCORDPARAMLABID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPARAMLABD', @level2type = N'COLUMN', @level2name = N'ID';

