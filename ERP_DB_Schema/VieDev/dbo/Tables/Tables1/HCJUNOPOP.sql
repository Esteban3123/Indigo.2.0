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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de Plantillas NO POS por Producto  Consecutivo Interno {00000021}', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOP', @level2type = N'COLUMN', @level2name = N'CODTIPPLAN';

