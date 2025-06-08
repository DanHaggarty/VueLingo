interface HistoryItem {
  input: string;
  output: string;
  tone: string;
  language: string | null;
  timestamp: string;
}

export function useHistory() {
  const STORAGE_KEY = 'rewriteHistory';

  const getHistory = (): HistoryItem[] => {
    const raw = sessionStorage.getItem(STORAGE_KEY);
    return raw ? JSON.parse(raw) : [];
  };

  const saveToHistory = (entry: HistoryItem): void => {
    const current = getHistory();
    current.unshift(entry); // newest first
    sessionStorage.setItem(STORAGE_KEY, JSON.stringify(current));
  };

  const clearHistory = (): void => {
    sessionStorage.removeItem(STORAGE_KEY);

  };

  return { getHistory, saveToHistory, clearHistory };
}
