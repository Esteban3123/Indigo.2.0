#!/usr/bin/env python3
"""Isolated checks for AppliesCatalogPropertyandServices persistence.

These tests do not need the ERP, DevExpress or a shared database.
They lock the conversion contract of ConvertYesNoEditValue and the
source mappings that previously dropped the Sí/No value.
"""
from __future__ import annotations

import pathlib
import re
import sys
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[2]


def convert_yes_no_edit_value(edit_value):
    """Mirrors FrmCompanySettings.ConvertYesNoEditValue."""
    if edit_value is None:
        return False
    if isinstance(edit_value, bool):
        return edit_value
    text = str(edit_value)
    try:
        return int(text) != 0
    except (TypeError, ValueError):
        pass
    lowered = text.casefold()
    if lowered in {"si", "sí", "true"}:
        return True
    if lowered in {"no", "false"}:
        return False
    raise ValueError(f"Unsupported Yes/No value: {edit_value!r}")


class ConvertYesNoEditValueTests(unittest.TestCase):
    def test_boolean_true(self):
        self.assertTrue(convert_yes_no_edit_value(True))

    def test_boolean_false(self):
        self.assertFalse(convert_yes_no_edit_value(False))

    def test_none_is_false(self):
        self.assertFalse(convert_yes_no_edit_value(None))

    def test_integer_one_is_true(self):
        self.assertTrue(convert_yes_no_edit_value(1))

    def test_integer_zero_is_false(self):
        self.assertFalse(convert_yes_no_edit_value(0))

    def test_string_si_is_true(self):
        self.assertTrue(convert_yes_no_edit_value("Si"))
        self.assertTrue(convert_yes_no_edit_value("Sí"))

    def test_string_no_is_false(self):
        self.assertFalse(convert_yes_no_edit_value("No"))


class SourceMappingTests(unittest.TestCase):
    def test_form_uses_dedicated_true_false_datasource(self):
        form = (
            ROOT
            / "ERP_Presentation"
            / "Presentation.Accounting"
            / "Utilities"
            / "FrmCompanySettings.vb"
        ).read_text(encoding="utf-8", errors="replace")
        self.assertIn("CatalogYesNoTuple", form)
        self.assertIn(
            "INDSleAppliesCatalogPropertyandServices.Properties.DataSource = CatalogYesNoTuple",
            form,
        )
        self.assertNotIn(
            "INDSleAppliesCatalogPropertyandServices.Properties.DataSource = TransactionEconomicActivityTuple",
            form,
        )
        self.assertIn("New Tuple(Of Boolean, String)(True, \"Si\")", form)
        self.assertIn("ConvertYesNoEditValue", form)
        self.assertIn("companySettings.StartTracking()", form)
        self.assertIn(".AppliesCatalogPropertyandServices = AppliesCatalogPropertyandServices", form)
        self.assertIn(
            "AppliesCatalogPropertyandServices = companySettings.AppliesCatalogPropertyandServices",
            form,
        )
        self.assertIn("CreateCompanySettingsSavePayload", form)
        self.assertIn("CreateCompanySettingsSavePayload(companySettings)", form)

    def test_service_maps_ef6_concurrency_without_success(self):
        service = (
            ROOT
            / "ERP_Services"
            / "Application.Accounting"
            / "CompanySettings"
            / "CompanySettingsAdminService.vb"
        ).read_text(encoding="utf-8", errors="replace")
        self.assertIn("Catch ex As DbUpdateConcurrencyException", service)
        self.assertIn("settings.StartTracking()", service)
        self.assertIn("ApplyIncomingCompanySettings", service)
        self.assertIn("FirstOrDefault(Function(x) x.Id = settings.Id, True, Nothing)", service)
        self.assertIn("target.AppliesCatalogPropertyandServices = source.AppliesCatalogPropertyandServices", service)
        self.assertNotIn("target.TimeStamp = source.TimeStamp", service)
        self.assertNotIn("target.ConsecutiveFiling = source.ConsecutiveFiling", service)
        self.assertIn("HasAccountPayable()", service)
        self.assertIn("Nullable.Equals(currentSettings.ModificationDate, settings.ModificationDate)", service)
        self.assertNotIn("TicksPerSecond", service)
        repository = ROOT.joinpath(
            "ERP_Services",
            "Infrastructure.Data.ModelRepository",
            "Repositories",
            "Accounting",
            "CompanySettingsRepository.vb",
        ).read_text(encoding="utf-8", errors="replace")
        self.assertIn("Function HasAccountPayable()", repository)
        self.assertIn("AccountPayable.AsNoTracking().Any()", repository)
        entity = ROOT.joinpath(
            "ERP_Services", "Domain.Entities", "CompanySettings.vb"
        ).read_text(encoding="utf-8", errors="replace")
        self.assertIn("Public Property AppliesCatalogPropertyandServices()", entity)

    def test_edmx_maps_company_settings_column(self):
        edmx = (
            ROOT
            / "ERP_Services"
            / "Infrastructure.Data.ModelRepository"
            / "Model"
            / "GlobalModel.edmx"
        ).read_text(encoding="utf-8", errors="replace")
        company_fragment = edmx.split('EntitySetMapping Name="CompanySettings"', 1)[1]
        company_fragment = company_fragment.split("EntitySetMapping", 1)[0]
        self.assertIn(
            'ScalarProperty Name="AppliesCatalogPropertyandServices" ColumnName="AppliesCatalogPropertyandServices"',
            company_fragment,
        )
        self.assertRegex(
            edmx,
            r'<EntityType Name="CompanySettings">[\s\S]*?<Property Name="AppliesCatalogPropertyandServices" Type="Boolean" Nullable="false" />',
        )

    def test_sql_column_definition(self):
        sql = (
            ROOT
            / "VieCloud_DB_Colombia_Schema"
            / "Vie_ERP"
            / "GeneralLedger"
            / "Tables"
            / "CompanySettings.sql"
        ).read_text(encoding="utf-8", errors="replace")
        self.assertRegex(
            sql,
            r"\[AppliesCatalogPropertyandServices\]\s+BIT\s+CONSTRAINT \[DF_CompanySettings_AppliesCatalogPropertyandServices\] DEFAULT \(\(0\)\) NOT NULL",
        )

    def test_success_message_only_when_state_result_true(self):
        form = (
            ROOT
            / "ERP_Presentation"
            / "Presentation.Accounting"
            / "Utilities"
            / "FrmCompanySettings.vb"
        ).read_text(encoding="utf-8", errors="replace")
        guardar = form.split("Public Async Sub Guardar()", 1)[1].split("End Sub", 1)[0]
        self.assertIn("If Result.StateResult = True Then", guardar)
        self.assertIn('ResourceManager.GetString("UpdateMessage")', guardar)
        failure_branch = guardar.split("If Result.StateResult = True Then", 1)[1]
        failure_branch = failure_branch.split("Else", 2)[-1]
        self.assertIn("Mensaje(EeventViewerImages.MensajeError)", failure_branch)
        self.assertNotIn('ResourceManager.GetString("UpdateMessage")', failure_branch)
        self.assertNotIn('ResourceManager.GetString("SaveMessage")', failure_branch)


if __name__ == "__main__":
    suite = unittest.defaultTestLoader.loadTestsFromModule(sys.modules[__name__])
    result = unittest.TextTestRunner(verbosity=2).run(suite)
    sys.exit(0 if result.wasSuccessful() else 1)
