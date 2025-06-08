import { saveAs } from 'file-saver'
import { useHistory } from '@/composables/useHistory'

export function useExportCsv() {
    const { getHistory } = useHistory()

    function escapeCsv(value: string): string {
        if (!value) return ''
        return '"' + value.replace(/"/g, '""').replace(/\n/g, ' ') + '"'
    }

    function exportHistoryToCsv() {
        const history = getHistory()
        if (!history.length) {
            alert('No history to export.')
            return
        }

        const headers = ['Timestamp', 'Tone', 'Language', 'Input', 'Output']
        const csvRows = [headers.join(',')]

        history.forEach(item => {
            const row = [
                escapeCsv(new Date(item.timestamp).toLocaleString()),
                escapeCsv(item.tone),
                escapeCsv(item.language || 'None'),
                escapeCsv(item.input),
                escapeCsv(item.output)
            ]
            csvRows.push(row.join(','))
        })

        const blob = new Blob([csvRows.join('\r\n')], { type: 'text/csv;charset=utf-8;' })
        saveAs(blob, `rewrite_history_${new Date().toISOString()}.csv`)
    }

    return { exportHistoryToCsv }
}
