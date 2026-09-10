Imports System
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TestDbConcurrencyMode

    <TestMethod()> Public Sub TestCrystalRepository()
        Dim cmt = New EFConcurrencyModeTest.ConcurrencyModeTester()
        Dim result = cmt.BadConcurrencyModes("..\..\..\Infrastructure.Data.CrystalRepository\Model\CrystalModel.edmx")
        Assert.AreNotEqual(Nothing, result, "No fue posible establecer un resultado de la validacion")
        If result IsNot Nothing Then
            Dim errors = result.Length
            Assert.AreEqual(0, errors, "Se encontraron " & errors & " configuraciones de concurrencia optimista erroneas")
        End If
    End Sub

    <TestMethod()> Public Sub TestDocumentalSystemRepository()
        Dim cmt = New EFConcurrencyModeTest.ConcurrencyModeTester()
        Dim result = cmt.BadConcurrencyModes("..\..\..\Infrastructure.Data.DocumentalSystemRepository\Model\DocumentalSystemModel.edmx")
        Assert.AreNotEqual(Nothing, result, "No fue posible establecer un resultado de la validacion")
        If result IsNot Nothing Then
            Dim errors = result.Length
            Assert.AreEqual(0, errors, "Se encontraron " & errors & " configuraciones de concurrencia optimista erroneas")
        End If
    End Sub

    <TestMethod()> Public Sub TestInteropCostRepository()
        Dim cmt = New EFConcurrencyModeTest.ConcurrencyModeTester()
        Dim result = cmt.BadConcurrencyModes("..\..\..\Infrastructure.Data.InteropCostRepository\Model\InteropCostModel.edmx")
        Assert.AreNotEqual(Nothing, result, "No fue posible establecer un resultado de la validacion")
        If result IsNot Nothing Then
            Dim errors = result.Length
            Assert.AreEqual(0, errors, "Se encontraron " & errors & " configuraciones de concurrencia optimista erroneas")
        End If
    End Sub

    <TestMethod()> Public Sub TestMaintenanceRepository()
        Dim cmt = New EFConcurrencyModeTest.ConcurrencyModeTester()
        Dim result = cmt.BadConcurrencyModes("..\..\..\Infrastructure.Data.MaintenanceRepository\Model\MaintenanceModel.edmx")
        Assert.AreNotEqual(Nothing, result, "No fue posible establecer un resultado de la validacion")
        If result IsNot Nothing Then
            Dim errors = result.Length
            Assert.AreEqual(0, errors, "Se encontraron " & errors & " configuraciones de concurrencia optimista erroneas")
        End If
    End Sub

    <TestMethod()> Public Sub TestModelRepository()
        Dim cmt = New EFConcurrencyModeTest.ConcurrencyModeTester()
        Dim result = cmt.BadConcurrencyModes("..\..\..\Infrastructure.Data.ModelRepository\Model\GlobalModel.edmx")
        Assert.AreNotEqual(Nothing, result, "No fue posible establecer un resultado de la validacion")
        If result IsNot Nothing Then
            Dim errors = result.Length
            Assert.AreEqual(0, errors, "Se encontraron " & errors & " configuraciones de concurrencia optimista erroneas")
        End If
    End Sub

    <TestMethod()> Public Sub TestPayrollRepository()
        Dim cmt = New EFConcurrencyModeTest.ConcurrencyModeTester()
        Dim result = cmt.BadConcurrencyModes("..\..\..\Infrastructure.Data.PayrollRepository\Model\PayrollModel.edmx")
        Assert.AreNotEqual(Nothing, result, "No fue posible establecer un resultado de la validacion")
        If result IsNot Nothing Then
            Dim errors = result.Length
            Assert.AreEqual(0, errors, "Se encontraron " & errors & " configuraciones de concurrencia optimista erroneas")
        End If
    End Sub

    <TestMethod()> Public Sub TestSecurityRepository()
        Dim cmt = New EFConcurrencyModeTest.ConcurrencyModeTester()
        Dim result = cmt.BadConcurrencyModes("..\..\..\Infrastructure.Data.SecurityRepository\Model\SecurityModel.edmx")
        Assert.AreNotEqual(Nothing, result, "No fue posible establecer un resultado de la validacion")
        If result IsNot Nothing Then
            Dim errors = result.Length
            Assert.AreEqual(0, errors, "Se encontraron " & errors & " configuraciones de concurrencia optimista erroneas")
        End If
    End Sub

End Class