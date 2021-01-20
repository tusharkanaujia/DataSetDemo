Imports System.Configuration
Imports System.Data.SqlClient

Module CalanderManager

    Sub Main()
        Dim conn As SqlConnection
        Dim cmdCalander As SqlCommand
        Dim conString As String

        conString = ConfigurationManager.ConnectionStrings("CalanderManagerConnectionString").ConnectionString
        conn = New SqlConnection(conString)
        conn.Open()
        cmdCalander = New SqlCommand

        'USING UNTYPED DATASET
        Dim adpCalander As SqlDataAdapter = New SqlDataAdapter

        With cmdCalander
            .Connection = conn
            .CommandTimeout = 30
            .CommandType = CommandType.Text
            .CommandText = "SELECT DateKey, FullDate, IsHoliday FROM Calander"
        End With

        adpCalander.SelectCommand = cmdCalander
        Dim dsCalander As DataSet = New DataSet
        adpCalander.Fill(dsCalander, "Calander")

        cmdCalander.CommandText = "SELECT DateKey, CountryHoliday FROM Holiday"
        adpCalander.Fill(dsCalander, "Holiday")
        Dim counter As Int32 = 0

        For Each holiday As DataRow In dsCalander.Tables("Holiday").Rows
            For Each calander As DataRow In dsCalander.Tables("Calander").Rows
                If calander.ItemArray(0) = holiday.ItemArray(0) Then
                    dsCalander.Tables("Calander").Rows(counter)(2) = True
                End If
                counter = counter + 1
            Next
        Next
        'dsCalander.Tables(0).AcceptChanges()
        adpCalander.Update(dsCalander, "Calander")

        'USING TYPED DATASET
        Dim adpCalanderTyped As CalanderDataSetTableAdapters.CalanderTableAdapter = New CalanderDataSetTableAdapters.CalanderTableAdapter
        Dim CalanderTable As CalanderDataSet.CalanderDataTable = New CalanderDataSet.CalanderDataTable
        adpCalanderTyped.Fill(CalanderTable)

        Dim adpHolidayTyped As CalanderDataSetTableAdapters.HolidayTableAdapter = New CalanderDataSetTableAdapters.HolidayTableAdapter
        Dim HolidayTable As CalanderDataSet.HolidayDataTable = New CalanderDataSet.HolidayDataTable
        adpHolidayTyped.Fill(HolidayTable)
        counter = 0
        For Each holiday As DataRow In HolidayTable.Rows
            For Each calander As DataRow In CalanderTable.Rows
                If calander.ItemArray(0) = holiday.ItemArray(0) Then
                    CalanderTable.Rows(counter)(2) = True
                End If
                counter = counter + 1
            Next
        Next
        CalanderTable.AcceptChanges()
        adpCalanderTyped.Update(CalanderTable)
        conn.Close()
    End Sub

End Module
