CREATE TABLE [dbo].[CHPARAMIN] (
    [CODCENATE] CHAR (10) NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    [INTASIGNA] BIT       NOT NULL,
    [INTEGRESA] BIT       NOT NULL,
    [INTTRASLA] BIT       NOT NULL,
    CONSTRAINT [PK_CHPARAMIN] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC),
    CONSTRAINT [FK_CHPARAMIN_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHPARAMIN_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[CHPARAMIN] NOCHECK CONSTRAINT [FK_CHPARAMIN_ADCENATEN];


GO
ALTER TABLE [dbo].[CHPARAMIN] NOCHECK CONSTRAINT [FK_CHPARAMIN_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se hace interfaz al trasladar la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMIN', @level2type = N'COLUMN', @level2name = N'INTTRASLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se hace interfaz al egresar de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMIN', @level2type = N'COLUMN', @level2name = N'INTEGRESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se hace interfaz al asignar camas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMIN', @level2type = N'COLUMN', @level2name = N'INTASIGNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMIN', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMIN', @level2type = N'COLUMN', @level2name = N'CODCENATE';

