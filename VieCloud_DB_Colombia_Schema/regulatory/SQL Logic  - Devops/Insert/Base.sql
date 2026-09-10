-- =============================================
-- RegulatoryEngine — Seed data inicial
-- Pack: CO-RIPS-FEV-948-2026  |  Regla: RVG14
-- Ejecutar UNA SOLA VEZ en cada ambiente (DEV, QA, PROD)
-- =============================================

INSERT INTO RegulatoryEngine.RegulatoryPack
(
    Code,
    Name,
    JurisdictionCode,
    Version,
    EffectiveFrom,
    IsActive
)
VALUES
(
    'CO-RIPS-FEV-948-2026',
    'Colombia RIPS FEV Resolucion 948',
    'CO',
    '2026.1',
    '2026-06-01',
    1
);

INSERT INTO RegulatoryEngine.RegulatoryRule
(
    RegulatoryPackId,
    RuleCode,
    RuleName,
    EngineClass,
    BlockingLevel,
    IsActive
)
SELECT
    rp.Id,
    'RVC005',
    'Coverage Classification Compatibility',
    'CoverageClassificationCompatibilityRule',
    'BLOCK',
    1
FROM RegulatoryEngine.RegulatoryPack rp
WHERE rp.Code = 'CO-RIPS-FEV-948-2026';

INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'DecisionTableName',
    'UserEntityCompatibility',
    'STRING'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC005';

INSERT INTO RegulatoryEngine.DecisionTable
(
    RegulatoryRuleId,
    Name,
    Description,
    IsActive
)
SELECT
    rr.Id,
    'UserEntityCompatibility',
    'Compatibilidad entre Tipo Usuario y Tipo Entidad Responsable de Pago',
    1
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC005';

INSERT INTO RegulatoryEngine.DecisionTableColumn
(
    DecisionTableId,
    ColumnName,
    ColumnOrder,
    DataType
)
SELECT
    dt.Id,
    'UserType',
    1,
    'INT'
FROM RegulatoryEngine.DecisionTable dt
WHERE dt.Name = 'UserEntityCompatibility';

INSERT INTO RegulatoryEngine.DecisionTableColumn
(
    DecisionTableId,
    ColumnName,
    ColumnOrder,
    DataType
)
SELECT
    dt.Id,
    'EntityType',
    2,
    'INT'
FROM RegulatoryEngine.DecisionTable dt
WHERE dt.Name = 'UserEntityCompatibility';

DECLARE @DecisionTableId BIGINT;

SELECT @DecisionTableId = Id
FROM RegulatoryEngine.DecisionTable
WHERE Name = 'UserEntityCompatibility';

INSERT INTO RegulatoryEngine.DecisionTableRow
(
    DecisionTableId,
    IsAllowed,
    EffectiveFrom
)
VALUES
(@DecisionTableId,1,'2026-06-01'), -- 1
(@DecisionTableId,1,'2026-06-01'), -- 2
(@DecisionTableId,1,'2026-06-01'), -- 3
(@DecisionTableId,1,'2026-06-01'), -- 4
(@DecisionTableId,1,'2026-06-01'), -- 5
(@DecisionTableId,1,'2026-06-01'), -- 6
(@DecisionTableId,1,'2026-06-01'), -- 7
(@DecisionTableId,1,'2026-06-01'), -- 8
(@DecisionTableId,1,'2026-06-01'), -- 9
(@DecisionTableId,1,'2026-06-01'), -- 10
(@DecisionTableId,1,'2026-06-01'), -- 11
(@DecisionTableId,1,'2026-06-01'), -- 12
(@DecisionTableId,1,'2026-06-01'), -- 13
(@DecisionTableId,1,'2026-06-01'), -- 14
(@DecisionTableId,1,'2026-06-01'), -- 15
(@DecisionTableId,1,'2026-06-01'); -- 16

DECLARE @UserTypeColumnId BIGINT;
DECLARE @EntityTypeColumnId BIGINT;

SELECT @UserTypeColumnId = Id
FROM RegulatoryEngine.DecisionTableColumn
WHERE ColumnName = 'UserType';

SELECT @EntityTypeColumnId = Id
FROM RegulatoryEngine.DecisionTableColumn
WHERE ColumnName = 'EntityType';

;WITH RowsOrdered AS
(
    SELECT
        ROW_NUMBER() OVER(ORDER BY Id) AS RN,
        Id
    FROM RegulatoryEngine.DecisionTableRow
    WHERE DecisionTableId =
    (
        SELECT Id
        FROM RegulatoryEngine.DecisionTable
        WHERE Name='UserEntityCompatibility'
    )
)

INSERT INTO RegulatoryEngine.DecisionTableCell
(
    DecisionTableRowId,
    DecisionTableColumnId,
    Value
)
SELECT Id,@UserTypeColumnId,'1'
FROM RowsOrdered WHERE RN=1

UNION ALL
SELECT Id,@EntityTypeColumnId,'1'
FROM RowsOrdered WHERE RN=1

UNION ALL

SELECT Id,@UserTypeColumnId,'2'
FROM RowsOrdered WHERE RN=2

UNION ALL
SELECT Id,@EntityTypeColumnId,'2'
FROM RowsOrdered WHERE RN=2

UNION ALL

SELECT Id,@UserTypeColumnId,'3'
FROM RowsOrdered WHERE RN=3

UNION ALL
SELECT Id,@EntityTypeColumnId,'3'
FROM RowsOrdered WHERE RN=3

UNION ALL

SELECT Id,@UserTypeColumnId,'3'
FROM RowsOrdered WHERE RN=4

UNION ALL
SELECT Id,@EntityTypeColumnId,'4'
FROM RowsOrdered WHERE RN=4

UNION ALL

SELECT Id,@UserTypeColumnId,'3'
FROM RowsOrdered WHERE RN=5

UNION ALL
SELECT Id,@EntityTypeColumnId,'11'
FROM RowsOrdered WHERE RN=5

UNION ALL

SELECT Id,@UserTypeColumnId,'3'
FROM RowsOrdered WHERE RN=6

UNION ALL
SELECT Id,@EntityTypeColumnId,'12'
FROM RowsOrdered WHERE RN=6

UNION ALL

SELECT Id,@UserTypeColumnId,'4'
FROM RowsOrdered WHERE RN=7

UNION ALL
SELECT Id,@EntityTypeColumnId,'99'
FROM RowsOrdered WHERE RN=7

UNION ALL

SELECT Id,@UserTypeColumnId,'4'
FROM RowsOrdered WHERE RN=8

UNION ALL
SELECT Id,@EntityTypeColumnId,'7'
FROM RowsOrdered WHERE RN=8

UNION ALL

SELECT Id,@UserTypeColumnId,'4'
FROM RowsOrdered WHERE RN=9

UNION ALL
SELECT Id,@EntityTypeColumnId,'8'
FROM RowsOrdered WHERE RN=9

UNION ALL

SELECT Id,@UserTypeColumnId,'9'
FROM RowsOrdered WHERE RN=10

UNION ALL
SELECT Id,@EntityTypeColumnId,'9'
FROM RowsOrdered WHERE RN=10

UNION ALL

SELECT Id,@UserTypeColumnId,'10'
FROM RowsOrdered WHERE RN=11

UNION ALL
SELECT Id,@EntityTypeColumnId,'12'
FROM RowsOrdered WHERE RN=11

UNION ALL

SELECT Id,@UserTypeColumnId,'11'
FROM RowsOrdered WHERE RN=12

UNION ALL
SELECT Id,@EntityTypeColumnId,'5'
FROM RowsOrdered WHERE RN=12

UNION ALL

SELECT Id,@UserTypeColumnId,'12'
FROM RowsOrdered WHERE RN=13

UNION ALL
SELECT Id,@EntityTypeColumnId,'10'
FROM RowsOrdered WHERE RN=13

UNION ALL

SELECT Id,@UserTypeColumnId,'12'
FROM RowsOrdered WHERE RN=14

UNION ALL
SELECT Id,@EntityTypeColumnId,'13'
FROM RowsOrdered WHERE RN=14

UNION ALL

SELECT Id,@UserTypeColumnId,'13'
FROM RowsOrdered WHERE RN=15

UNION ALL
SELECT Id,@EntityTypeColumnId,'6'
FROM RowsOrdered WHERE RN=15

UNION ALL

SELECT Id,@UserTypeColumnId,'13'
FROM RowsOrdered WHERE RN=16

UNION ALL
SELECT Id,@EntityTypeColumnId,'13'
FROM RowsOrdered WHERE RN=16;


--RVC057
INSERT INTO RegulatoryEngine.RegulatoryRule
(
    RegulatoryPackId,
    RuleCode,
    RuleName,
    EngineClass,
    BlockingLevel,
    IsActive
)
SELECT
    rp.Id,
    'RVC057',
    'Gestational Age Range Validation',
    'ClinicalObservationRangeRule',
    'BLOCK',
    1
FROM RegulatoryEngine.RegulatoryPack rp
WHERE rp.Code = 'CO-RIPS-FEV-948-2026';

INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'ObservationType',
    'GestationalAge',
    'STRING'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC057';

INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'MinimumValue',
    '0',
    'INTEGER'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC057';

INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'MaximumValue',
    '45',
    'INTEGER'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC057';

INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'Unit',
    'Week',
    'STRING'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC057';


INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'ObservationValueSet',
    'GestationalAgeObservation',
    'VALUESET'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC057';

--RVC058
--1. RegulatoryRule
INSERT INTO RegulatoryEngine.RegulatoryRule
(
    RegulatoryPackId,
    RuleCode,
    RuleName,
    EngineClass,
    BlockingLevel,
    IsActive
)
SELECT
    rp.Id,
    'RVC058',
    'Newborn Birth Weight Range Validation',
    'ClinicalObservationRangeRule',
    'BLOCK',
    1
FROM RegulatoryEngine.RegulatoryPack rp
WHERE rp.Code = 'CO-RIPS-FEV-948-2026';

--2. RuleParameter
--ObservationType
INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'ObservationType',
    'BirthWeight',
    'STRING'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC058';

--MinimumValue
INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'MinimumValue',
    '500',
    'INTEGER'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC058';

--MaximumValue
INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'MaximumValue',
    '5000',
    'INTEGER'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC058';

--Unit
INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'Unit',
    'Gram',
    'STRING'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC058';


INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'ObservationValueSet',
    'BirthWeightObservation',
    'VALUESET'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC058';

--RVC084
--RegulatoryRule
INSERT INTO RegulatoryEngine.RegulatoryRule
(
    RegulatoryPackId,
    RuleCode,
    RuleName,
    EngineClass,
    BlockingLevel,
    IsActive
)
SELECT
    rp.Id,
    'RVC084',
    'Purpose Of Care Validation',
    'TerminologyMembershipRule',
    'BLOCK',
    1
FROM RegulatoryEngine.RegulatoryPack rp
WHERE rp.Code = 'CO-RIPS-FEV-948-2026';

--TerminologyValueSet
INSERT INTO RegulatoryEngine.TerminologyValueSet
(
    Name,
    Description,
    JurisdictionCode,
    Version,
    IsActive
)
VALUES
(
    'ValidPurposeOfCare',
    'Allowed purpose of care codes for RVC084',
    'CO',
    '2026.1',
    1
);

--TerminologyValueSetItem
DECLARE @ValueSetId BIGINT;

SELECT @ValueSetId = Id
FROM RegulatoryEngine.TerminologyValueSet
WHERE Name = 'ValidPurposeOfCare';
--Carga de códigos validos
INSERT INTO RegulatoryEngine.TerminologyValueSetItem
(
    TerminologyValueSetId,
    CodeSystem,
    Code,
    DisplayName,
    JurisdictionCode,
    IsActive
)
VALUES

(@ValueSetId,'FINALIDAD_ATENCION','15','Diagnóstico','CO',1),

(@ValueSetId,'FINALIDAD_ATENCION','16','Tratamiento','CO',1),

(@ValueSetId,'FINALIDAD_ATENCION','17','Rehabilitación','CO',1),

(@ValueSetId,'FINALIDAD_ATENCION','18','Paliación','CO',1),

(@ValueSetId,'FINALIDAD_ATENCION','43','Modificación de la estética corporal','CO',1),

(@ValueSetId,'FINALIDAD_ATENCION','44','Otra','CO',1);


--RuleParameter
INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'PurposeOfCareValueSet',
    'ValidPurposeOfCare',
    'VALUESET'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC084';


--RVC053
--1. RegulatoryRule
INSERT INTO RegulatoryEngine.RegulatoryRule
(
    RegulatoryPackId,
    RuleCode,
    RuleName,
    EngineClass,
    BlockingLevel,
    IsActive
)
SELECT
    rp.Id,
    'RVC053',
    'Post Mortem Service Window Validation',
    'PostMortemServiceWindowRule',
    'BLOCK',
    1
FROM RegulatoryEngine.RegulatoryPack rp
WHERE rp.Code = 'CO-RIPS-FEV-948-2026';

--2. RuleParameter
--Ventana permitida

INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'AllowedPostDeathServiceWindowHours',
    '24',
    'INTEGER'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC053';


--Campo paciente
INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'PatientIdentifierField',
    'IPCODPACI',
    'STRING'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC053';

--Campo ingreso
INSERT INTO RegulatoryEngine.RuleParameter
(
    RegulatoryRuleId,
    ParameterName,
    ParameterValue,
    DataType
)
SELECT
    rr.Id,
    'EncounterIdentifierField',
    'NUMINGRES',
    'STRING'
FROM RegulatoryEngine.RegulatoryRule rr
WHERE rr.RuleCode = 'RVC053';

--RVC062

INSERT INTO RegulatoryEngine.RegulatoryRule
(
    RegulatoryPackId,
    RuleCode,
    RuleName,
    EngineClass,
    BlockingLevel,
    IsActive
)
SELECT
    rp.Id,
    'RVC062',
    'Referral requires services provided',
    'ReferralRequiresServices',
    'BLOCK',
    1
FROM RegulatoryEngine.RegulatoryPack rp
WHERE rp.Code = 'CO-RIPS-FEV-948-2026';


