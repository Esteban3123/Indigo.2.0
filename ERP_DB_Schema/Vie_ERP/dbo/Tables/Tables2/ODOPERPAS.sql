CREATE TABLE [dbo].[ODOPERPAS] (
    [ID]         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROL]  INT             NOT NULL,
    [NUMDIENTE]  TINYINT         NOT NULL,
    [MARGEN1]    DECIMAL (18, 1) NULL,
    [MARGEN2]    DECIMAL (18, 1) NULL,
    [MARGEN3]    DECIMAL (18, 1) NULL,
    [SURCO1]     DECIMAL (18, 1) NULL,
    [SURCO2]     DECIMAL (18, 1) NULL,
    [SURCO3]     DECIMAL (18, 1) NULL,
    [INSERCION1] DECIMAL (18, 1) NULL,
    [INSERCION2] DECIMAL (18, 1) NULL,
    [INSERCION3] DECIMAL (18, 1) NULL,
    [SANGRADO1]  BIT             NULL,
    [SANGRADO2]  BIT             NULL,
    [SANGRADO3]  BIT             NULL,
    [FURCACION1] CHAR (1)        NULL,
    [FURCACION2] CHAR (1)        NULL,
    CONSTRAINT [PK_ODOPERIPAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODOPERIPAS_ODOPERICTR] FOREIGN KEY ([IDCONTROL]) REFERENCES [dbo].[ODOPERCTR] ([ID])
);


GO
ALTER TABLE [dbo].[ODOPERPAS] NOCHECK CONSTRAINT [FK_ODOPERIPAS_ODOPERICTR];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Furcación2    1: Furca Inicial  2: Furca abierta  3: Furca Completa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'FURCACION2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Furcación1     1: Furca Inicial  2: Furca abierta  3: Furca Completa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'FURCACION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Sangrado3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Sangrado2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Sangrado1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SANGRADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Insercion3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Insercion2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Insercion1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'INSERCION1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Surco3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Surco2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Surco1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'SURCO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Margen3  del grupo  Superficie Lingual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda margen2  del grupo  Superficie Vestibular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda margen1  del grupo  Superficie Palatina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'MARGEN1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Numero del diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de control de periodontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERPAS', @level2type = N'COLUMN', @level2name = N'ID';

