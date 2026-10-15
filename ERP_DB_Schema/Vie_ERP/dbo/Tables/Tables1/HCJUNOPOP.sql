CREATE TABLE [dbo].[HCJUNOPOP] (
    [CODPRODUC]  CHAR (20)                                                                    NOT NULL,
    [CODTIPPLAN] CHAR (5)                                                                     NOT NULL,
    [DESTIPPLAN] CHAR (200)                                                                   NOT NULL,
    [INDTERAPR]  VARCHAR (MAX)                                                                NULL,
    [RESJUSMED]  VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "SummaryHC_Ofuscado", 0)') NULL,
    [EFEADVMED]  VARCHAR (MAX)                                                                NULL,
    [RAZVENMED]  VARCHAR (MAX)                                                                NULL,
    [RIEINMPAC]  BIT                                                                          NULL,
    [AGOPOSTER]  BIT                                                                          NULL,
    [MEDUTIPAI]  BIT                                                                          NULL,
    [MEDEXPERI]  BIT                                                                          NULL,
    [USOCORINV]  BIT                                                                          NULL,
    [INDAUDFOR]  NUMERIC (18)                                                                 NOT NULL,
    CONSTRAINT [PK_HCJUNOPOP] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC, [CODTIPPLAN] ASC),
    CONSTRAINT [FK_HCJUNOPOP_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCJUNOPOP] NOCHECK CONSTRAINT [FK_HCJUNOPOP_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El uso corresponde a la indicación registrada en el Invima ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'USOCORINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es un medicamento de experimentación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'MEDEXPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es un medicamento utilizado en el pais', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'MEDUTIPAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se han agotado las posibilidades terapéuticas existentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'AGOPOSTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Existe Riesgo Inminente para la Vida del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RIEINMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Explicacion de Razones y Ventajas del uso de este Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RAZVENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posibles Efectos Adversos que se deriven del uso de este medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'EFEADVMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resumen de la Historia Clinica y Justificacion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'RESJUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones Terapeuticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'INDTERAPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Plantilla Justificacion Medicamento No POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'DESTIPPLAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de Plantillas NO POS por Producto  Consecutivo Interno {00000021}', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'CODTIPPLAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Medicamento NO POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'CODPRODUC';

