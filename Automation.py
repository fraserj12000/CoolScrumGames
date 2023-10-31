from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import Select
import unittest
import time

class Test(unittest.TestCase):
    def setUp(self):
        self.chrome_options = Options()
        self.options = [
            "--headless",
            "--disable-gpu",
            "--window-size=1920,1200",
            "--ignore-certificate-errors",
            "--disable-extensions",
            "--no-sandbox",
            "--disable-dev-shm-usage"
        ]
        for self.option in self.options:
          self.chrome_options.add_argument(self.option)
        
        self.driver = webdriver.Chrome(options=self.chrome_options)
        
        self.driver.get("http://127.0.0.1:32825")
        
        self.driver.implicitly_wait(0.5)

    def test_checkNavbar(self):
        self.navitems = self.driver.find_elements(By.XPATH, "/html/body/header/nav/div/div/ul/li")
        self.assertEqual(self.navitems[0].text, "Desktop Games")

        self.navitems = self.driver.find_elements(By.XPATH, "/html/body/header/nav/div/div/ul/li")
        self.assertEqual(self.navitems[1].text, "Mobile Games")

        self.navitems = self.driver.find_elements(By.XPATH, "/html/body/header/nav/div/div/ul/li")
        self.assertEqual(self.navitems[2].text, "About Us")

        self.navitems = self.driver.find_elements(By.XPATH, "/html/body/header/nav/div/div/ul/li")
        self.assertEqual(self.navitems[3].text, "Privacy")
    
    def test_checkJakesGame(self):
        self.jakesgame = self.driver.find_element(By.XPATH, "/html/body/div/main/div[2]/div[1]/h3/button")
        self.jakesgame.click()
        self.driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
        time.sleep(2)
        self.playbutton = self.driver.find_element(By.XPATH, "/html/body/div/main/button")
        self.assertEqual(self.playbutton.text, "Play Game")
        self.playbutton.click()
        self.canvas = self.driver.find_elements(By.XPATH, '//*[@id="gameCanvas"]')
        assert self.canvas[0] is not None

    def test_checkSnakeGame(self):
        self.snakegame = self.driver.find_element(By.XPATH, "/html/body/div/main/div[2]/div[2]/h3/button")
        self.snakegame.click()
        self.driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
        time.sleep(2)
        self.playbutton = self.driver.find_element(By.XPATH, "/html/body/div/main/button")
        self.assertEqual(self.playbutton.text, "Play Game")
        self.playbutton.click()
        self.canvas = self.driver.find_elements(By.XPATH, '//*[@id="gameCanvas"]')
        assert self.canvas[0] is not None

    def tearDown(self):
        self.driver.quit()

if __name__ == '__main__':
    unittest.main()
