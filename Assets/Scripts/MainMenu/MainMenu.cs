using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private RectTransform _mainMenu;
        [SerializeField] private RectTransform _setting;
/*        private void Start()
        {
            m_Continue.interactable = FileHandler.HaveFile(MapCompletion.filename);
        }*/

        public void NewGame()
        {
/*            FileHandler.Reset(MapCompletion.filename);
            FileHandler.Reset(Upgrades.filename);*/
            SceneManager.LoadScene(1);
        }

        public void Setting()
        {
            _mainMenu.gameObject.SetActive(false);
            _setting.gameObject.SetActive(true);
        }

        /*        public void Continue()
                {
                    SceneManager.LoadScene(1);
                }*/

        public void ReturMainMenu()
        {
            _mainMenu.gameObject.SetActive(true);
            _setting.gameObject.SetActive(false);
        }

        public void Quit()
        { 
            Application.Quit();
        }

    }
}

