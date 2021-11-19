//작성자 조현우
using UnityEditor;
using UnityEngine;

namespace GameEditor
{

    public class DeletePlayerPrefs
    {
        [MenuItem("Tools/SaveData/Delete PlayerPrefs", false, 1)]
        public static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
