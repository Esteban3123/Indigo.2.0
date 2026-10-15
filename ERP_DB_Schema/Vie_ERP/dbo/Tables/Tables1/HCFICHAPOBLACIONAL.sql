CREATE TABLE [dbo].[HCFICHAPOBLACIONAL] (
    [ID]                  INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT      NOT NULL,
    [CODPOBLACION]        TINYINT  NOT NULL,
    [CODDIAGNO]           CHAR (4) NULL,
    CONSTRAINT [PK_HCFICHA110P] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHAPOBLACIONAL_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHAPOBLACIONAL] NOCHECK CONSTRAINT [FK_HCFICHAPOBLACIONAL_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de población:   1=Discapacitados   2=Desplazados   3=Migrantes   4=Carcelarios   5=Gestantes   6=Indigentes   7=Población infaltil a cargo del ICBF   8=Madres comunitarias   9=Desmovilizados   10=Centros psiquiátricos   11=Víctimas de violencia armada   12=Otros grupos poblacionales   13=Indigenas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'CODPOBLACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el ID  de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'ID';

