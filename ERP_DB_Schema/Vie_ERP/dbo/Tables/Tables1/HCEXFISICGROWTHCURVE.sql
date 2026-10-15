CREATE TABLE [dbo].[HCEXFISICGROWTHCURVE] (
    [ID]                       INT             IDENTITY (1, 1) NOT NULL,
    [IDhcfisic]                INT             NOT NULL,
    [AgeGestationalDiagram]    VARBINARY (MAX) NULL,
    [WeightForLengthDiagram]   VARBINARY (MAX) NULL,
    [BodyMassIndexDiagram]     VARBINARY (MAX) NULL,
    [WeightForAgeDiagram]      VARBINARY (MAX) NULL,
    [HeadCircumferenceDiagram] VARBINARY (MAX) NULL,
    [LengthForAgeDiagram]      VARBINARY (MAX) NULL,
    [UterineHeightDiagram]     VARBINARY (MAX) NULL,
    CONSTRAINT [PK__HCEXFISI__3214EC277BAB0038] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCEXFISICGROWTHCURVE_HCEXFISIC] FOREIGN KEY ([IDhcfisic]) REFERENCES [dbo].[HCEXFISIC] ([NUMCONSEC])
);


GO
ALTER TABLE [dbo].[HCEXFISICGROWTHCURVE] NOCHECK CONSTRAINT [FK_HCEXFISICGROWTHCURVE_HCEXFISIC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda altura Uterina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'UterineHeightDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda longitud ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'LengthForAgeDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda circuferencia de la cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'HeadCircumferenceDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda peso para la edad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'WeightForAgeDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda masa de indice corporal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'BodyMassIndexDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  peso de Longitud ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'WeightForLengthDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda código edad gestacional diagrama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'AgeGestationalDiagram';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el id historia examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'IDhcfisic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXFISICGROWTHCURVE', @level2type = N'COLUMN', @level2name = N'ID';

