CREATE TABLE [dbo].[CruceMaquinas] (
    [Serial]        VARCHAR (50)  NULL,
    [Placa]         VARCHAR (50)  NULL,
    [Clasificacion] VARCHAR (50)  NULL,
    [Responsable]   VARCHAR (100) NULL,
    [Oficina]       VARCHAR (100) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la oficina de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Oficina';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el responsable de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Responsable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la clasificación de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Clasificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la placa de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Placa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el serial de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Serial';

