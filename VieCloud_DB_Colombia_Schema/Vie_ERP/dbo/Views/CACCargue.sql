CREATE VIEW [dbo].[CACCargue]
AS
SELECT '' AS Expr1, CODDIAGNO, [ID Entidad VIE], [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], [13], [14], [15], [16], [17], [18], [19], [20], [21], [22], [23], [24], [25], [26], [27], [28], [29], [30], [31], [32], [33], [34], [35], [36], [37], [38], [39], [40], [41], [42], [43], [44], [45], 
                  [46], [46#1], [46#2], [46#3], [46#4], [46#5], [46#6], [46#7], [46#8], [47], [48], [49], [50], [51], [52], [53], [53#1], [53#2], [53#3], [53#4], [53#5], [53#6], [53#7], [53#8], [53#9], [53#10], [53#11], [53#12], [53#13], [53#14], [53#15], [53#16], [53#17], [53#18], [53#19], 
                  [53#20], [53#21], [53#22], [53#23], [53#24], [53#25], [53#26], [53#27], [53#28], [53#29], [53#30], [53#31], [53#32], [54], [55], [56], [57], [58], [59], [60], [61], [62], [63], [64], [65], [66], [66#1], [66#2], [66#3], [66#4], [66#5], [66#6], [66#7], [66#8], [66#9], [66#10], 
                  [66#11], [66#12], [66#13], [66#14], [66#15], [66#16], [66#17], [66#18], [66#19], [66#20], [66#21], [66#22], [66#23], [66#24], [66#25], [66#26], [66#27], [66#28], [66#29], [66#30], [66#31], [66#32], [67], [68], [69], [70], [71], [72], [73], [74], [75], [76], [77], [78], [79], 
                  [80], [81], [82], [83], [84], [85], [86], [87], [88], [89], [90], [91], [92], [93], [94], [95], [96], [97], [98], [99], [100], [101], [102], [103], [104], [105], [106], [107], [108], [109], [110], [111], [112], [113], [114], [114#1], [114#2], [114#3], [114#4], [114#5], [114#6], [115], [116], 
                  [117], [118], [119], [120], [121], [122], [123], [124], [125], [126], [127], [128], [129], [130], [131], [132], [133], [134], [135], [136], [137], [138], [139], [140], [141], [142], [143], [144], [145], [146], [147], [148], [149], [150], [151], [152], [153], [154], [155], [156], [157], 
                  [158], [159], [160], [161], [162], [163], [164], [165], [166], [167], [168], [169], [170], [171], [172]
FROM     dbo.CAC
WHERE  (CODDIAGNO NOT IN ('D752', 'C80X', 'C952'))
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de cargue o exportación de datos de la tabla CAC, que contiene información estructurada por columnas numeradas (campos 1 al 172, incluyendo variantes como 46#1, 53#n, 66#n, 114#n) asociadas a diagnósticos CIE-10 (CODDIAGNO) y a una entidad de la plataforma VIE (ID Entidad VIE). Filtra y excluye los diagnósticos ''D752'', ''C80X'' y ''C952'', entregando solo los registros válidos para el proceso de cargue. Su propósito es preparar y exponer la información de la tabla CAC en un formato tabular plano con múltiples atributos por diagnóstico, utilizado posiblemente para cargue masivo, migración o reportería estructurada hacia sistemas externos o procesos de integración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CACCargue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CACCargue';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los registros de la cuenta de alto costo (CAC) excluyendo diagnósticos no reportables, agregando una columna vacía inicial para alinear el formato de cargue.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CACCargue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla origen debe contener la columna CODDIAGNO y el conjunto numerado de columnas (1..172 con sufijos #) requerido por el formato de cargue.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CACCargue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se exponen filas con diagnósticos D752, C80X o C952.; La primera columna del resultado siempre es una cadena vacía (placeholder de formato).; Se preserva el orden y la nomenclatura de columnas numeradas (incluyendo variantes con sufijo #N) tal como las espera el formato de cargue externo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CACCargue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta de Alto Costo (CAC); Diagnóstico (CODDIAGNO); Entidad VIE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CACCargue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CAC: Devuelve todas las filas cuyo CODDIAGNO no esté en (''D752'',''C80X'',''C952'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CACCargue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODDIAGNO IN (''D752'',''C80X'',''C952'') → Se excluye la fila del resultado else Se incluye la fila en el cargue', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CACCargue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CACCargue';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'CACCargue';
GO
