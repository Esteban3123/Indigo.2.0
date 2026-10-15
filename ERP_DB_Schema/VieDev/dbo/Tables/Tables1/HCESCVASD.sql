CREATE TABLE [dbo].[HCESCVASD] (
    [CODCONSEC] NUMERIC (18) NOT NULL,
    [CODTIPDOL] TINYINT      NOT NULL,
    [CODPARCUE] TINYINT      NOT NULL,
    [PTSESTREP] TINYINT      NOT NULL,
    [PTSESTMOV] TINYINT      NOT NULL,
    [POSIMGCUX] INT          NOT NULL,
    [POSIMGCUY] INT          NOT NULL,
    CONSTRAINT [PK_HCESCVASD] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC, [CODPARCUE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parte del cuerpo  0: Cabeza Anterior  1: Cuello Anterior  2: Miembro Superior Anterior Derecho  3: Torax  4: Miembro Superior Anterior Izquierdo  5: Abdomen  6: Pelvis  7: Miembro Inferior Anterior Derecho  8: Miembro Inferior Anterior Izquierdo  9: Cabeza Posterior  10: Cuello Posterior  11: Miembro Superior Posterior Izquierdo  12: Region Dorsal  13: Miembro Superior Posterior Derecho  14: Region Lumbar  15: Region Glutea  16: Miembro Inferior Posterior Izquierdo  17: Miembro Inferior Posterior Derecho  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODPARCUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de dolor  0  ''Sordo''  1 ''Punzante / Cortante  2  ''Palpitante''  3  ''Quemante''  4  ''Hormigueo, Burbujeo  5 ''Opresivo''  6 ''Adormecido''  7 ''Calambres''   8 ''Dolorido''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODTIPDOL';

