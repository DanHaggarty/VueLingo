export function useClipboard() {
    const copyToClipboard = async (text: string): Promise<boolean> => {
        if (!navigator.clipboard) {
            console.warn('Clipboard API not supported.')
            return false
        }

        try {
            await navigator.clipboard.writeText(text)
            return true
        } catch (error) {
            console.error('Clipboard copy failed:', error)
            return false
        }
    }

    return { copyToClipboard }
}
